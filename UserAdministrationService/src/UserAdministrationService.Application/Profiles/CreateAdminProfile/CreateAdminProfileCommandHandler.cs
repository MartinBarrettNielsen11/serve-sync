using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreateAdminProfile;

internal sealed class CreateAdminProfileCommandHandler(IUsersRepository usersRepository)
	: IRequestHandler<CreateAdminProfileCommand, Result<Guid>>
{
	public async ValueTask<Result<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
	{
		User? user = await usersRepository.GetByIdAsync(command.UserId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<Guid>(Error.NotFound("UserNotFound", "User not found"));
		}

		Result<Guid> instructorId = user.CreateInstructorProfile();

		await usersRepository.UpdateAsync(user, cancellationToken);

		return instructorId;
	}
}
