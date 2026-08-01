namespace Analyzers;


[Shared]
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SealedClassCodeFix))]
public sealed class SealedClassCodeFixProvider : CodeFixProvider
{
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

        var classDeclaration = root?.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();

        if (classDeclaration == null)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Make the class sealed",
                createChangedDocument: _ => MakeClassSealedAsync(root!, context.Document, classDeclaration),
                equivalenceKey: nameof(SealedClassCodeFix)),
            diagnostic);
    }

    private static Task<Document> MakeClassSealedAsync(SyntaxNode root, Document document, ClassDeclarationSyntax @class)
    {
        var newClass = @class.WithModifiers(@class.Modifiers.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword)));

        var newRoot = root.ReplaceNode(@class, newClass);

        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }

    public override ImmutableArray<string> FixableDiagnosticIds => [SealedClassAnalyzer.RuleId];
}
