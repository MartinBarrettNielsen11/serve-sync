using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyAnalyzer =
    Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier<
        Analyzers.SealedClassAnalyzer,
        Microsoft.CodeAnalysis.Testing.DefaultVerifier>;
using VerifyCodeFix =
    Microsoft.CodeAnalysis.CSharp.Testing.CSharpCodeFixTest<
        Analyzers.SealedClassAnalyzer,
        Analyzers.CodeFix.SealedClassCodeFix,
        Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace Analyzers.Tests;

public sealed class SealedClassAnalyzerTests
{
    [Fact]
    public async Task Success()
    {
        const string source = """
                              using System;

                              public class SUT
                              {
                              }
                              """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(3, 14).WithArguments("SUT");

        await VerifyAnalyzer.VerifyAnalyzerAsync(source, expected);
    }

    [Fact]
    public async Task When_ClassHasSealedModifier_Then_NoDiagnosticIsReported()
    {
        const string source = """
                              public sealed class SUT
                              {
                              }
                              """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassHasAbstractModifier_Then_NoDiagnosticIsReported()
    {
        const string source = """
                              public abstract class SUT
                              {
                              }
                              """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassHasStaticModifier_Then_NoDiagnosticIsProduced()
    {
        const string source = """
                              public static class SUT
                              {
                              }
                              """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassIsDerived_Then_NoDiagnosticIsProduced()
    {
        const string source = """
                              public class SUT
                              {
                              }

                              public sealed class SUT2 : SUT
                              {
                              }
                              """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_MultipleCandidatesExistInOneFile_Then_MultipleDiagnosticsAreReported()
    {
        const string source = """
                              public class SUT1
                              {
                              }

                              internal class SUT2
                              {
                              }
                              """;

        DiagnosticResult firstDiagnostic = VerifyAnalyzer.Diagnostic().WithLocation(1, 14).WithArguments("SUT1");
        DiagnosticResult secondDiagnostic = VerifyAnalyzer.Diagnostic().WithLocation(5, 16).WithArguments("SUT2");

        await VerifyAnalyzer.VerifyAnalyzerAsync(source, expected: [firstDiagnostic, secondDiagnostic]);
    }

    [Fact]
    public async Task When_ApplyingCodeFix_Then_SealedModifierIsAdded()
    {
        const string source = """
                              using System;

                              public class SUT
                              {
                              }
                              """;

        const string fixedCode = """
                                 using System;

                                 public sealed class SUT
                                 {
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(3, 14).WithArguments("SUT");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task When_ApplyingCodeFix_Then_ExistingModifierIsKept()
    {
        const string source = """
                              internal class SUT
                              {
                              }
                              """;

        const string fixedCode = """
                                 internal sealed class SUT
                                 {
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(1, 16).WithArguments("SUT");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task When_ApplyingCodeFix_Then_ClassMembersAreKeptTheSame()
    {
        const string source = """
                              public class SUT
                              {
                                  public string Name { get; set; } = string.Empty;

                                  public void Execute()
                                  {
                                  }
                              }
                              """;

        const string fixedCode = """
                                 public sealed class SUT
                                 {
                                     public string Name { get; set; } = string.Empty;

                                     public void Execute()
                                     {
                                     }
                                 }
                                 """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(1, 14).WithArguments("SUT");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
