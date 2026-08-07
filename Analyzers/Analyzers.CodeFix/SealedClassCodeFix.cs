using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Analyzers.CodeFix;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SealedClassCodeFix))]
internal sealed class SealedClassCodeFix : CodeFixProvider
{
	public override ImmutableArray<string> FixableDiagnosticIds => [SealedClassAnalyzer.RuleId];

	public override FixAllProvider GetFixAllProvider()
	{
		return WellKnownFixAllProviders.BatchFixer;
	}

	public override async Task RegisterCodeFixesAsync(CodeFixContext context)
	{
		Diagnostic diagnostic = context.Diagnostics.First();
		TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

		SyntaxNode? syntaxNode = await context.Document.GetSyntaxRootAsync(context.CancellationToken);

		ClassDeclarationSyntax? classDeclaration = syntaxNode?
													.FindToken(diagnosticSpan.Start)
													.Parent
													?
													.AncestorsAndSelf()
													.OfType<ClassDeclarationSyntax>()
													.FirstOrDefault();

		if (classDeclaration is null)
		{
			return;
		}

		context.RegisterCodeFix(CodeAction.Create("Make the class sealed",
												_ => MakeClassSealedAsync(syntaxNode!,
																		context.Document,
																		classDeclaration),
												nameof(SealedClassCodeFix)),
								diagnostic);
	}

	private static Task<Document> MakeClassSealedAsync(SyntaxNode syntaxNode, Document document,
														ClassDeclarationSyntax classDeclaration)
	{
		ClassDeclarationSyntax newClass =
			classDeclaration.WithModifiers(classDeclaration.Modifiers
															.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword)));

		SyntaxNode newSyntaxNode = syntaxNode.ReplaceNode(classDeclaration, newClass);

		Document createChangedDocument = document.WithSyntaxRoot(newSyntaxNode);

		return Task.FromResult(createChangedDocument);
	}
}
