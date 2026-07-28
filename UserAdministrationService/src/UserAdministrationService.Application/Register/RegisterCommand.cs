using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;

namespace UserAdministrationService.Application.Register;

internal sealed record RegisterCommand(string FirstName, string LastName, string Email, string Password) :
    IRequest<Result<AuthenticationResult>>;
