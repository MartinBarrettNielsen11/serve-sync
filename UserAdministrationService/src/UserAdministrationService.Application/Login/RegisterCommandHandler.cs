using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Login;

internal sealed class RegisterCommandHandler(IUsersRepository usersRepository, PasswordHasher passwordHasher)
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal async ValueTask<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // check of user exists
        var userExists = await usersRepository.ExistsByEmailAsync(command.Email);

        if (!userExists)
        {
            return Result.Failure(Error.Conflict(code: "UserAlreadyExists", description: "User already exists"));
        }
        
        var hashPasswordResult = passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsError)
        {
            return hashPasswordResult.Errors;
        }
        
        User user = new(command.FirstName, command.LastName, command.Email, passwordHash: hashPasswordResult);

        await usersRepository.AddUserAsync(user);
        
        // return some authentication dto including a token
        return null!;
    }
}