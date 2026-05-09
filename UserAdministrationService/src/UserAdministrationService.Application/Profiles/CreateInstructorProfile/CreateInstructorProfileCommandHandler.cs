using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Application.Profiles.CreateTrainerProfile;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreateInstructorProfile;

internal sealed class CreateInstructorProfileCommandHandler(IUsersRepository usersRepository)
{
    internal async ValueTask<Result> Handle(CreateInstructorProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Result.Failure(Error.NotFound(code: "UserNotFound", description: "User not found"));
        }
        
        Result<Guid> createInstructorProfileResult = user.CreateInstructorProfile();

        await usersRepository.UpdateAsync(user);

        return createInstructorProfileResult;
    }
}