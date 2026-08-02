using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealedClassAnalyzer : DiagnosticAnalyzer
{
    internal const string RuleId = "RULE0001";

    private static readonly DiagnosticDescriptor Rule = new(
        RuleId,
        title: "Class can be sealed",
        messageFormat: "Class '{0}' can be sealed",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.None);

        context.RegisterCompilationStartAction(compilationContext =>
        {
            Lazy<HashSet<INamedTypeSymbol>> inheritedTypes = new(
                () => FindInheritedTypes(compilationContext.Compilation));

            compilationContext.RegisterSyntaxNodeAction(
                action: syntaxContext => AnalyzeClass(
                    syntaxContext,
                    inheritedTypes.Value),
                syntaxKinds: SyntaxKind.ClassDeclaration);
        });
    }

    private static void AnalyzeClass(
        SyntaxNodeAnalysisContext context,
        HashSet<INamedTypeSymbol> inheritedTypes)
    {
        ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.Node;

        INamedTypeSymbol? classSymbol = context.SemanticModel.GetDeclaredSymbol(
            declarationSyntax: classSyntax, context.CancellationToken);

        if (classSymbol is null)
        {
            return;
        }

        if (!CanBeSealed(classSymbol, inheritedTypes))
        {
            return;
        }

        Diagnostic diagnostic = Diagnostic.Create(
            Rule,
            classSyntax.Identifier.GetLocation(),
            classSymbol.Name);

        context.ReportDiagnostic(diagnostic);
    }

    private static bool CanBeSealed(
        INamedTypeSymbol type,
        HashSet<INamedTypeSymbol> inheritedTypes)
    {
        if (type.TypeKind != TypeKind.Class || type.IsRecord)
        {
            return false;
        }

        if (type.IsAbstract || type.IsSealed || type.IsStatic)
        {
            return false;
        }

        return !inheritedTypes.Contains(type.OriginalDefinition);
    }

    private static HashSet<INamedTypeSymbol> FindInheritedTypes(
        Compilation compilation)
    {
        List<INamedTypeSymbol> declaredTypes = new();

        CollectTypes(compilation.Assembly.GlobalNamespace, declaredTypes);

        HashSet<INamedTypeSymbol> inheritedTypes = new(comparer: SymbolEqualityComparer.Default);

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
