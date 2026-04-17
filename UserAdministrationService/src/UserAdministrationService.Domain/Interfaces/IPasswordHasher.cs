using SharedKernel.Results;

namespace UserAdministrationService.Domain.Interfaces;

internal interface IPasswordHasher
{
    public Result<string> HashPassword(string password);
    bool IsCorrectPassword(string password, string hash);
}