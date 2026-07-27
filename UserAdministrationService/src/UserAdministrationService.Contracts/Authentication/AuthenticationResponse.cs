namespace UserAdministrationService.Contracts.Authentication;

internal sealed record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
