using MediatR;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Login;

internal sealed class LoginQueryHandler(IPasswordHasher passwordHasher, 
                                        IUsersRepository usersRepository,
                                        IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        User? user = await usersRepository.GetByEmailAsync(query.Email, cancellationToken);

        if (user is null || !user.IsCorrectPasswordHash(query.Password, passwordHasher))
        {
            return Result.Failure<AuthenticationResult>(AuthenticationErrors.InvalidCredentials);
        }
        
        return new AuthenticationResult(user, jwtTokenGenerator.GenerateToken(user));
    }
}