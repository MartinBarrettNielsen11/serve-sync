using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Login;

internal sealed class RegisterCommandHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher)
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
        
        Result<string> hashPasswordResult = passwordHasher.HashPassword(command.Password);

        if (hashPasswordResult.IsFailure)
        {
            return Result.Failure(hashPasswordResult.Error);
        }
        
        User user = new(firstName: command.FirstName, 
                        lastName: command.LastName, 
                        email: command.Email, 
                        passwordHash: hashPasswordResult.Value);

        await usersRepository.AddUserAsync(user);
        
        // return some authentication dto including a token
        return null!;
    }
}