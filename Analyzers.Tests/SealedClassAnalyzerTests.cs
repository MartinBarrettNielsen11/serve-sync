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
    public async Task ConcreteClass_ShouldProduceDiagnostic()
    {
        const string testCode = $$"""
                                  using System;

                                  public class MyClass
                                  {
                                  }
                                  """;

        DiagnosticResult expected = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(3, 14)
            .WithArguments("MyClass");

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            source: testCode,
            expected: expected);
    }


    [Fact]
    public async Task ClassThatCannotOrShouldNotBeSealed_ShouldNotProduceDiagnostic()
    {
        const string testCode = $$"""
                                  public sealed class MyClass
                                  {
                                  }
                                  """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task ClassThatCannotOrShouldNotBeSealed_ShouldNotProduceDiagnostic_2()
    {
        const string testCode = $$"""
                                  public static class MyClass
                                  {
                                  }
                                  """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
#pragma warning disable S4144
    public async Task ClassThatCannotOrShouldNotBeSealed_ShouldNotProduceDiagnostic_3()
#pragma warning restore S4144
    {
        const string testCode = $$"""
                                  public static class MyClass
                                  {
                                  }
                                  """;
        await VerifyAnalyzer.VerifyAnalyzerAsync(testCode);
    }

    [Fact]
    public async Task MultipleConcreteClasses_ShouldProduceMultipleDiagnostics()
    {
        const string testCode = $$"""
                                  public class FirstClass
                                  {
                                  }

                                  internal class SecondClass
                                  {
                                  }
                                  """;

        DiagnosticResult firstDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(1, 14)
            .WithArguments("FirstClass");

        DiagnosticResult secondDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(5, 16)
            .WithArguments("SecondClass");

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            testCode,
            firstDiagnostic,
            secondDiagnostic);
    }

    [Fact]
    public async Task ConcreteClass_CodeFixShouldAddSealedModifier()
    {
        const string testCode = $$"""
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
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task InternalClass_CodeFixShouldPreserveExistingModifier()
    {
        const string testCode = $$"""
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
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }

    [Fact]
    public async Task ClassWithMembers_CodeFixShouldOnlyChangeClassDeclaration()
    {
        const string testCode = $$"""
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
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
