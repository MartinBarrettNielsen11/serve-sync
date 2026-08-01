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
            .WithLocation(3, 8);

        await VerifyAnalyzer.VerifyAnalyzerAsync(
            testCode,
            expected);
    }

    [Theory]
    [InlineData(
        $$"""
          public sealed class MyClass
          {
          }
          """)]
    [InlineData(
        $$"""
          public abstract class MyClass
          {
          }
          """)]
    [InlineData(
        $$"""
          public static class MyClass
          {
          }
          """)]
    public async Task ClassThatCannotOrShouldNotBeSealed_ShouldNotProduceDiagnostic(
        string testCode)
    {
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
            .WithLocation(1, 8);

        DiagnosticResult secondDiagnostic = VerifyAnalyzer
            .Diagnostic()
            .WithLocation(5, 10);

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
            .WithLocation(3, 8);

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
            .WithLocation(1, 10);

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
            .WithLocation(1, 8);

        VerifyCodeFix test = new()
        {
            TestCode = testCode,
            FixedCode = fixedCode
        };

        test.ExpectedDiagnostics.Add(expected);

        await test.RunAsync();
    }
}
