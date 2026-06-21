using System.Text.RegularExpressions;
using SharedKernel.Results;
using UserAdministrationService.Domain.Interfaces;

namespace UserAdministrationService.Infrastructure.Authentication;

internal partial class PasswordHasher : IPasswordHasher
{
    private static readonly Regex PasswordRegex = StrongPasswordRegex();
    
    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();

    public Result<string> HashPassword(string password)
    {
        return !PasswordRegex.IsMatch(password)
            ? Result.Failure<string>(Error.Failure(code: "", description: "Password too weak"))
            : Result.Success<string>(BCrypt.Net.BCrypt.EnhancedHashPassword(password));
    }
    
    public bool IsCorrectPassword(string password, string hash) => BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
}
