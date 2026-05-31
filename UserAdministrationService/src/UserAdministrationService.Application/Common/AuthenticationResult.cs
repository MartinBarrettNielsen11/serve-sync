using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Common;

internal sealed record AuthenticationResult(User User, string TokenPlaceHolder);