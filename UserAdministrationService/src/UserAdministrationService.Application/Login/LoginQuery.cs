using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;

namespace UserAdministrationService.Application.Login;

public sealed record LoginQuery(string Email, string Password) : IRequest<Result<AuthenticationResult>>;