using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreateAdminProfile;

internal sealed class CreateAdminProfileCommandHandler(IUsersRepository usersRepository)
{
    internal async ValueTask<Result<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Result.Failure<Guid>(Error.NotFound(code: "UserNotFound", description: "User not found"));
        }

        Result<Guid> instructorId = user.CreateInstructorProfile();

        await usersRepository.UpdateAsync(user);

        return instructorId;
    }
}