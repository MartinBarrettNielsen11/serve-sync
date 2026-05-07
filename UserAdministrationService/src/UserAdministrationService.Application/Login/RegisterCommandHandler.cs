using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Login;

internal sealed class RegisterCommandHandler(IUsersRepository usersRepository)
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal async ValueTask<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)

    {
        // check of user exists
        
        User user = new(command.FirstName, command.LastName, command.Email, passwordHash: "update this");

        await usersRepository.AddUserAsync(user);
        
        // return some authentication dto including a token
        return null!;
    }
}