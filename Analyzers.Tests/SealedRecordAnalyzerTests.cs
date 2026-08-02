using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyAnalyzer =
    Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier<
        Analyzers.SealedRecordAnalyzer,
        Microsoft.CodeAnalysis.Testing.DefaultVerifier>;
using VerifyCodeFix =
    Microsoft.CodeAnalysis.CSharp.Testing.CSharpCodeFixTest<
        Analyzers.SealedRecordAnalyzer,
        Analyzers.CodeFix.SealedRecordCodeFix,
        Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace Analyzers.Tests;

public sealed class SealedRecordAnalyzerTests
{
    [Fact]
    public async Task ConcreteRecord_ShouldProduceDiagnostic()
    {
        const string testCode = """
                                public record MyRecord
                                {
                                }
                                """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 15)
            .WithArguments("MyRecord");

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            testCode,
            expected);
    }

    [Fact]
    public async Task SealedRecord_ShouldNotProduceDiagnostic()
    {
        const string testCode = """
                                public sealed record MyRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task AbstractRecord_ShouldNotProduceDiagnostic()
    {
        const string testCode = """
                                public abstract record MyRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task RecordStruct_ShouldNotProduceDiagnostic()
    {
        const string testCode = """
                                public record struct MyRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task RecordWithDerivedRecord_ShouldNotProduceDiagnostic()
    {
        const string testCode = """
                                public record BaseRecord
                                {
                                }

                                public sealed record DerivedRecord : BaseRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task MultipleConcreteRecords_ShouldProduceMultipleDiagnostics()
    {
        const string testCode = """
                                public record FirstRecord
                                {
                                }

                                internal record SecondRecord
                                {
                                }
                                """;

        DiagnosticResult firstDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 15)
            .WithArguments("FirstRecord");

        DiagnosticResult secondDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(5, 17)
            .WithArguments("SecondRecord");

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            testCode,
            firstDiagnostic,
            secondDiagnostic);
    }

    [Fact]
    public async Task ConcreteRecord_CodeFixShouldAddSealedModifier()
    {
        const string testCode = """
                                public record MyRecord
                                {
                                }
                                """;

        const string fixedCode = """
                                 public sealed record MyRecord
                                 {
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 15)
            .WithArguments("MyRecord");

        VerifyCodeFix test = new()
        {
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task InternalRecord_CodeFixShouldPreserveExistingModifier()
    {
        const string testCode = """
                                internal record MyRecord
                                {
                                }
                                """;

        const string fixedCode = """
                                 internal sealed record MyRecord
                                 {
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 17)
            .WithArguments("MyRecord");

        VerifyCodeFix test = new()
        {
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task RecordWithMembers_CodeFixShouldOnlyChangeDeclaration()
    {
        const string testCode = """
                                public record MyRecord
                                {
                                    public string Name { get; init; } = string.Empty;

                                    public void Execute()
                                    {
                                    }
                                }
                                """;

        const string fixedCode = """
                                 public sealed record MyRecord
                                 {
                                     public string Name { get; init; } = string.Empty;

                                     public void Execute()
                                     {
                                     }
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 15)
            .WithArguments("MyRecord");

        VerifyCodeFix test = new()
        {
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
