using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SealedRecordAnalyzer : DiagnosticAnalyzer
{
    private const string RuleId = "RULE0002";

    private static readonly DiagnosticDescriptor Rule = new(
        RuleId,
        title: "The record have to be sealed",
        messageFormat: "Record can be sealed",
        category: "Unknown",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(AnalyzeSealedRecord, SyntaxKind.RecordDeclaration);
    }

    private static void AnalyzeSealedRecord(SyntaxNodeAnalysisContext context)
    {
        RecordDeclarationSyntax recordSyntax = (RecordDeclarationSyntax)context.Node;

        var skipRuleGeneration = recordSyntax.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.AbstractKeyword) ||
                                                                       modifier.IsKind(SyntaxKind.SealedKeyword) ||
                                                                       modifier.IsKind(SyntaxKind.StaticKeyword));

        if (skipRuleGeneration)
        {
            return;
        }

        Diagnostic diagnostic = Diagnostic.Create(Rule, recordSyntax.Keyword.GetLocation());
        context.ReportDiagnostic(diagnostic);
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];
}

/*
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SealedClassAnalyzer : DiagnosticAnalyzer
{
    internal const string RuleId = "RULE0001";

    private static readonly DiagnosticDescriptor Rule = new(
        RuleId,
        title: "The class have to be sealed",
        messageFormat: "Class can be sealed",
        category: "Unknown",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(_rule);

    public override void Initialize(AnalysisContext context)
    {
        // You must call this method to avoid analyzing generated code.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // You must call this method to enable the Concurrent Execution.
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeSymbol, SyntaxKind.IdentifierName);
    }

    private static readonly string[] _forbiddenPropertyNames = new[]
    {
        nameof(DateTime.Now), nameof(DateTime.Today), nameof(DateTime.UtcNow)
    };

    private static void AnalyzeSymbol(SyntaxNodeAnalysisContext context)
    {
        SyntaxToken token = context.Node.GetFirstToken();

        if (!token.ToString().Equals(nameof(DateTime), StringComparison.Ordinal))
        {
            return;
        }

        SyntaxToken nextToken = token.GetNextToken();

        if (!nextToken.IsKind(SyntaxKind.DotToken))
        {
            return;
        }

        SyntaxToken dateTimeProperty = nextToken.GetNextToken();
        foreach (var forbiddenPropertyName in _forbiddenPropertyNames)
        {
            if (!dateTimeProperty.ToString().Equals(forbiddenPropertyName, StringComparison.Ordinal))
            {
                continue;
            }

            context.ReportDiagnostic(Diagnostic.Create(_rule, context.Node.Parent!.GetLocation(), forbiddenPropertyName));
            break;
        }
    }
}
*/
