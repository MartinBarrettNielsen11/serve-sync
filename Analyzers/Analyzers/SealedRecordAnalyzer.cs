using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal sealed class SealedRecordAnalyzer : DiagnosticAnalyzer
{
	internal const string RuleId = "RULE0002";

	private static readonly DiagnosticDescriptor Rule = new(
		RuleId,
		"Record can be sealed",
		"Record '{0}' can be sealed",
		"Design",
		DiagnosticSeverity.Warning,
		true);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

	public override void Initialize(AnalysisContext context)
	{
		context.EnableConcurrentExecution();
		context.ConfigureGeneratedCodeAnalysis(
			GeneratedCodeAnalysisFlags.None);

		context.RegisterCompilationStartAction(compilationContext =>
		{
			Lazy<HashSet<INamedTypeSymbol>> inheritedTypes =
				new(() => FindInheritedTypes(compilationContext.Compilation));

			compilationContext.RegisterSyntaxNodeAction(
				syntaxContext => AnalyzeRecord(
					syntaxContext,
					inheritedTypes.Value),
				SyntaxKind.RecordDeclaration);
		});
	}

	private static void AnalyzeRecord(
		SyntaxNodeAnalysisContext context,
		HashSet<INamedTypeSymbol> inheritedTypes)
	{
		RecordDeclarationSyntax recordSyntax =
			(RecordDeclarationSyntax)context.Node;

		INamedTypeSymbol? recordSymbol =
			context.SemanticModel.GetDeclaredSymbol(
				recordSyntax,
				context.CancellationToken);

		if (recordSymbol is null)
		{
			return;
		}

		if (!CanBeSealed(recordSymbol, inheritedTypes))
		{
			return;
		}

		Diagnostic diagnostic = Diagnostic.Create(
			Rule,
			recordSyntax.Identifier.GetLocation(),
			recordSymbol.Name);

		context.ReportDiagnostic(diagnostic);
	}

	private static bool CanBeSealed(
		INamedTypeSymbol type,
		HashSet<INamedTypeSymbol> inheritedTypes)
	{
		// IsRecord includes record classes and record structs.
		// Only record classes can be sealed.
		if (!type.IsRecord || type.TypeKind != TypeKind.Class)
		{
			return false;
		}

		if (type.IsAbstract || type.IsSealed)
		{
			return false;
		}

		return !inheritedTypes.Contains(type.OriginalDefinition);
	}

	private static HashSet<INamedTypeSymbol> FindInheritedTypes(
		Compilation compilation)
	{
		List<INamedTypeSymbol> declaredTypes = new();

		CollectTypes(
			compilation.Assembly.GlobalNamespace,
			declaredTypes);

		HashSet<INamedTypeSymbol> inheritedTypes = new(
			SymbolEqualityComparer.Default);

#pragma warning disable S3267
		foreach (INamedTypeSymbol type in declaredTypes)
#pragma warning restore S3267
		{
			if (type.BaseType is not null)
			{
				inheritedTypes.Add(type.BaseType.OriginalDefinition);
			}
		}

		return inheritedTypes;
	}

	private static void CollectTypes(
		INamespaceSymbol namespaceSymbol,
		List<INamedTypeSymbol> types)
	{
		foreach (INamespaceSymbol childNamespace
				in namespaceSymbol.GetNamespaceMembers())
		{
			CollectTypes(childNamespace, types);
		}

		foreach (INamedTypeSymbol type
				in namespaceSymbol.GetTypeMembers())
		{
			CollectTypeAndNestedTypes(type, types);
		}
	}

	private static void CollectTypeAndNestedTypes(
		INamedTypeSymbol type,
		List<INamedTypeSymbol> types)
	{
		types.Add(type);

		foreach (INamedTypeSymbol nestedType in type.GetTypeMembers())
		{
			CollectTypeAndNestedTypes(nestedType, types);
		}
	}
}
