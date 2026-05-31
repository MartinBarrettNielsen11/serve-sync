using MediatR;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Login;

internal sealed class LoginQueryHandler(IPasswordHasher passwordHasher, 
                                        IUsersRepository usersRepository)
    : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await usersRepository.GetByEmailAsync(query.Email);

        return user is null || !user.IsCorrectPasswordHash(query.Password, passwordHasher)
            ? Result.Failure<AuthenticationResult>(AuthenticationErrors.InvalidCredentials)
            : new AuthenticationResult(user, user.Email.ToString()); // You need to fix this
    }
}