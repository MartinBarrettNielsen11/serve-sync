using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Register;

internal sealed class RegisterCommandHandler(IUsersRepository usersRepository,
											IPasswordHasher passwordHasher,
											IJwtTokenGenerator jwtTokenGenerator)
	: IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
	public async ValueTask<Result<AuthenticationResult>> Handle(RegisterCommand command,
																CancellationToken cancellationToken)
	{
		var userExists = await usersRepository.ExistsByEmailAsync(command.Email, cancellationToken);

		if (!userExists)
		{
			return Result.Failure<AuthenticationResult>(Error.Conflict("UserAlreadyExists", "User already exists"));
		}

		Result<string> hashPasswordResult = passwordHasher.HashPassword(command.Password);

		if (hashPasswordResult.IsFailure)
		{
			return Result.Failure<AuthenticationResult>(hashPasswordResult.Error);
		}

		User user = new(command.FirstName,
						command.LastName,
						command.Email,
						hashPasswordResult.Value);

		await usersRepository.AddUserAsync(user, cancellationToken);

		var token = jwtTokenGenerator.GenerateToken(user);

		AuthenticationResult result = new(user, token);

		return Result.Success(result);
	}
}
