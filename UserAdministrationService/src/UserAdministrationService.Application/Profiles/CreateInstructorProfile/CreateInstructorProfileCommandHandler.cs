using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreateInstructorProfile;

internal sealed class CreateInstructorProfileCommandHandler(IUsersRepository usersRepository)
	: IRequestHandler<CreateInstructorProfileCommand, Result<Guid>>
{
	public async ValueTask<Result<Guid>> Handle(CreateInstructorProfileCommand command,
		CancellationToken cancellationToken)
	{
		User? user = await usersRepository.GetByIdAsync(command.UserId, cancellationToken);

		if (user is null)
		{
			return Result.Failure<Guid>(Error.NotFound("UserNotFound", "User not found"));
		}

		Result<Guid> createInstructorProfileResult = user.CreateInstructorProfile();

		await usersRepository.UpdateAsync(user, cancellationToken);

		return createInstructorProfileResult;
	}
}
