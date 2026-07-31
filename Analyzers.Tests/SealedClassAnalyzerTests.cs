
using Microsoft.CodeAnalysis.Testing;
using Xunit;
using Verify = Microsoft.CodeAnalysis.CSharp.Testing.CSharpAnalyzerVerifier<
    Analyzers.SealedClassAnalyzer,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;
using Verify2 = Microsoft.CodeAnalysis.CSharp.Testing.CSharpCodeFixTest<
    Analyzers.SealedClassAnalyzer,
    Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

using static Microsoft.CodeAnalysis.Testing.DiagnosticResult;

namespace Analyzers.Tests;

public class SealedClassAnalyzerTests
{
    [Fact]
    public async Task Test()
    {
        const string testCode = $$"""
                     using System;

                     public class MyClass
                     {
                     }
                     """;
        var yo = Verify.
        DiagnosticResult expected = Verify.Diagnostic().WithLocation(1, 7);
        await Verify.VerifyAnalyzerAsync(testCode, expected);
    }

    [Fact]
    public async Task Test2()
    {
        const string testCode = @"original source code";
        DiagnosticResult expected = CompilerError("CS0246").WithLocation(1, 23).WithMessage("; expected");
        await Verify.VerifyAnalyzerAsync(testCode, expected);
    }
}

/*
public class DateTimeNowAnalyzerTests
{
    [Theory(Skip = "Skipped to avoid build issues on gated builds")]
    [InlineData(nameof(DateTime.Now))]
    [InlineData(nameof(DateTime.UtcNow))]
    [InlineData(nameof(DateTime.Today))]
    public async Task The_analyzer_must_find_the_usage_of_the_property(string propertyName)
    {
        var text = $$"""
                     using System;

                     public class MyClass
                     {
                         public DateTime GetDate(){
                             return DateTime.{{propertyName}};
                         }
                     }
                     """;

        var expected = Verifier.Diagnostic()
            .WithLocation(6, 16)
            .WithArguments(propertyName);
        await Verifier.VerifyAnalyzerAsync(text, expected).ConfigureAwait(false);
    }
}
*/
