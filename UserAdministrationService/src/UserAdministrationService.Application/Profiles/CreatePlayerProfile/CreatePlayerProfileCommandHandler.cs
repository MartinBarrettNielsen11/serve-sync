using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreatePlayerProfile;

internal sealed class CreatePlayerProfileCommandHandler(IUsersRepository usersRepository)
	: IRequestHandler<CreatePlayerProfileCommand, Result<Guid>>
{
	public async ValueTask<Result<Guid>> Handle(CreatePlayerProfileCommand command, CancellationToken cancellationToken)
	{
		User? user = await usersRepository.GetByIdAsync(command.UserId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<Guid>(Error.NotFound("UserNotFound", "User not found"));
		}

		Result<Guid> createParticipantProfileResult = user.CreatePlayerProfile();

		await usersRepository.UpdateAsync(user, cancellationToken);

		return createParticipantProfileResult;
	}
}
