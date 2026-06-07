namespace UserAdministrationService.Application.Register;

internal record RegisterCommand(string FirstName, string LastName, string Email, string Password);
