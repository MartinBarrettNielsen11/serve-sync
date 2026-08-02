using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Text;

namespace Analyzers.CodeFix;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SealedClassCodeFix))]
public sealed class SealedRecordCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => [SealedRecordAnalyzer.RuleId];

    public override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        Diagnostic diagnostic = context.Diagnostics.First();
        TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        ClassDeclarationSyntax? classDeclaration = root?
            .FindToken(diagnosticSpan.Start).Parent?
            .AncestorsAndSelf()
            .OfType<ClassDeclarationSyntax>()
            .FirstOrDefault();

        if (classDeclaration is null)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Make the class sealed",
                createChangedDocument: _ => MakeRecordSealedAsync(root!, context.Document, classDeclaration),
                equivalenceKey: nameof(SealedRecordCodeFix)),
            diagnostic);
    }

    private static Task<Document> MakeRecordSealedAsync(SyntaxNode root, Document document, ClassDeclarationSyntax @class)
    {
        ClassDeclarationSyntax newClass = @class.WithModifiers(@class.Modifiers.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword)));

        SyntaxNode newRoot = root.ReplaceNode(@class, newClass);

        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }
}
