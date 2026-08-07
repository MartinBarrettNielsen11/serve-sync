namespace UserAdministrationService.Contracts.Authentication;

internal sealed record LoginRequest(string Email,
									string Password);
