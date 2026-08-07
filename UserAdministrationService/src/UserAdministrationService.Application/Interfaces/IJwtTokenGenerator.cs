using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Interfaces;

internal interface IJwtTokenGenerator
{
	string GenerateToken(User user);
}
