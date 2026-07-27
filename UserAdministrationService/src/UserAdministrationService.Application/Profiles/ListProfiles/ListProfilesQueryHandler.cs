using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.ListProfiles;

internal sealed class ListProfilesQueryHandler(IUsersRepository usersRepository)
	: IRequestHandler<ListProfilesQuery, Result<ListProfilesResult>>
{
	public async ValueTask<Result<ListProfilesResult>> Handle(ListProfilesQuery query,
		CancellationToken cancellationToken)
	{
		User? user = await usersRepository.GetByIdAsync(query.UserId, cancellationToken);

		if (user is null)
        {
            return Result.Failure<ListProfilesResult>(Error.NotFound(description: "User not found",
                code: "UserNotFound"));
        }

        ListProfilesResult result = new(user.AdminId, user.PlayerId, user.InstructorId);

		return result;
	}
}
