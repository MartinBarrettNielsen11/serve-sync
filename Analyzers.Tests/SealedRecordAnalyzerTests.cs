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
    public async Task Success()
    {
        const string source = """
                                public record MyRecord
                                {
                                }
                                """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 15)
            .WithArguments("MyRecord");

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            source,
            expected);
    }

    [Fact]
    public async Task When_RecordHasSealedModifier_Then_NoDiagnosticIsProduced()
    {
        const string source = """
                              public sealed record MyRecord
                              {
                              }
                              """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_RecordHasAbstractModifier_Then_NoDiagnosticIsReported()
    {
        const string source = """
                                public abstract record MyRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_RecordHasStructModifier_Then_NoDiagnosticIsReported()
    {
        const string source = """
                                public record struct MyRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_RecordIsDerived_Then_NoDiagnosticIsReported()
    {
        const string source = """
                                public record BaseRecord
                                {
                                }

                                public sealed record DerivedRecord : BaseRecord
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_MultipleCandidatesExistInOneFile_Then_MultipleDiagnosticsAreReported()
    {
        const string source = """
                              public record SUT1
                              {
                              }

                              public record SUT2
                              {
                              }
                              """;

        DiagnosticResult firstDiagnostic = VerifyAnalyzer.Diagnostic().WithLocation(1, 15).WithArguments("SUT1");
        DiagnosticResult secondDiagnostic = VerifyAnalyzer.Diagnostic().WithLocation(5, 15).WithArguments("SUT2");

        await VerifyAnalyzer.VerifyAnalyzerAsync(source, expected: [firstDiagnostic, secondDiagnostic]);
    }


    [Fact]
    public async Task ConcreteRecord_CodeFixShouldAddSealedModifier()
    {
        const string source = """
                                public record SUT
                                {
                                }
                                """;

        const string fixedCode = """
                                 public sealed record SUT
                                 {
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(1, 15).WithArguments("SUT");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task InternalRecord_CodeFixShouldPreserveExistingModifier()
    {
        const string source = """
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
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task RecordWithMembers_CodeFixShouldOnlyChangeDeclaration()
    {
        const string source = """
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

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(1, 15).WithArguments("MyRecord");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
