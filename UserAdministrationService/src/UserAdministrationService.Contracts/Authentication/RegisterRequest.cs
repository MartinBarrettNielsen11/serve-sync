namespace UserAdministrationService.Contracts.Authentication;

internal sealed record RegisterRequest(
	string FirstName,
	string LastName,
	string Email,
	string Password);
