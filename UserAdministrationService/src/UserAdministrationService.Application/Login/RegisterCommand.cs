using System.ComponentModel.DataAnnotations;

namespace UserAdministrationService.Application.Login;

internal record RegisterCommand(string FirstName, string LastName, string Email, string Password);
