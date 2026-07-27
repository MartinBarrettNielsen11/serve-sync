using SharedKernel.Results;

namespace UserAdministrationService.Domain.Interfaces;

internal interface IPasswordHasher
{
    Result<string> HashPassword(string password);
	bool IsCorrectPassword(string password, string hash);
}
