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
        const string source = $$"""
                                  using System;

                                  public class SUT
                                  {
                                  }
                                  """;

        DiagnosticResult expected = VerifyAnalyzer.Diagnostic().WithLocation(3, 14).WithArguments("SUT");

        await VerifyAnalyzer.VerifyAnalyzerAsync(source, expected);
    }

    [Fact]
    public async Task TwoClasses_ShouldReportDiagnosticForBothClasses()
    {
        const string source = """
                                public class FirstClass
                                {
                                }

                                public class SecondClass
                                {
                                }
                                """;

        DiagnosticResult firstDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 14)
            .WithArguments("FirstClass");

        DiagnosticResult secondDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(5, 14)
            .WithArguments("SecondClass");

        await VerifyAnalyzer.VerifyAnalyzerAsync(source, expected: [firstDiagnostic, secondDiagnostic]);
    }

    [Fact]
    public async Task When_ClassIsDerived_Then_NoDiagnosticIsProduced()
    {
        const string source = """
                                public class BaseClass
                                {
                                }

                                public sealed class DerivedClass : BaseClass
                                {
                                }
                                """;

        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassHasSealedModifier_Then_NoDiagnosticIsReported()
    {
        const string source = $$"""
                                  public sealed class MyClass
                                  {
                                  }
                                  """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassHasAbstractModifier_Then_NoDiagnosticIsReported()
    {
        const string source = $$"""
                                public abstract class MyClass
                                {
                                }
                                """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }

    [Fact]
    public async Task When_ClassHasStaticModifier_Then_NoDiagnosticIsProduced()
    {
        const string source = $$"""
                                  public static class MyClass
                                  {
                                  }
                                  """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(source);
    }


    [Fact]
    public async Task When_MultipleCandidatesExistInOneFile_Then_MultipleDiagnosticsAreReported()
    {
        const string source = $$"""
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
        const string source = $$"""
                                  using System;

                                  public class MyClass
                                  {
                                  }
                                  """;

        const string fixedCode = $$"""
                                   using System;

                                   public sealed class MyClass
                                   {
                                   }
                                   """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(3, 14)
            .WithArguments("MyClass");

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
        const string source = $$"""
                                  internal class MyClass
                                  {
                                  }
                                  """;

        const string fixedCode = $$"""
                                   internal sealed class MyClass
                                   {
                                   }
                                   """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 16)
            .WithArguments("MyClass");

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
        const string source = $$"""
                                  public class MyClass
                                  {
                                      public string Name { get; set; } = string.Empty;

                                      public void Execute()
                                      {
                                      }
                                  }
                                  """;

        const string fixedCode = $$"""
                                   public sealed class MyClass
                                   {
                                       public string Name { get; set; } = string.Empty;

                                       public void Execute()
                                       {
                                       }
                                   }
                                   """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 14)
            .WithArguments("MyClass");

        VerifyCodeFix test = new()
        {
            TestCode = source,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
