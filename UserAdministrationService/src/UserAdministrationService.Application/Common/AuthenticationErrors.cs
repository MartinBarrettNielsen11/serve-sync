using SharedKernel.Results;

namespace UserAdministrationService.Application.Common;

public static class AuthenticationErrors
{
	internal static readonly Error InvalidCredentials = Error.Failure(
		"Authentication.InvalidCredentials",
		"Invalid credentials");
}