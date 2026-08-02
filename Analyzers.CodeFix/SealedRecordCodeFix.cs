using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Text;

namespace Analyzers.CodeFix;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SealedRecordCodeFix))]
public sealed class SealedRecordCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds => [SealedRecordAnalyzer.RuleId];

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        Diagnostic diagnostic = context.Diagnostics.First();
        TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

        SyntaxNode? syntaxNode = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        RecordDeclarationSyntax? recordDeclaration = syntaxNode?
            .FindToken(diagnosticSpan.Start).Parent?
            .AncestorsAndSelf()
            .OfType<RecordDeclarationSyntax>()
            .FirstOrDefault();

        if (recordDeclaration is null)
        {
            return;
        }

        context.RegisterCodeFix(
            action: CodeAction.Create(
                title: "Make the record sealed",
                createChangedDocument: _ => MakeRecordSealedAsync(syntaxNode!, context.Document, recordDeclaration),
                equivalenceKey: nameof(SealedRecordCodeFix)),
            diagnostic: diagnostic);
    }

    private static Task<Document> MakeRecordSealedAsync(SyntaxNode root, Document document, RecordDeclarationSyntax recordDeclaration)
    {
        RecordDeclarationSyntax newRecord = recordDeclaration.WithModifiers(recordDeclaration.Modifiers.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword)));

        SyntaxNode newSyntaxNode = root.ReplaceNode(recordDeclaration, newRecord);

        Document createChangedDocument = document.WithSyntaxRoot(newSyntaxNode);

        return Task.FromResult(createChangedDocument);
    }
}
