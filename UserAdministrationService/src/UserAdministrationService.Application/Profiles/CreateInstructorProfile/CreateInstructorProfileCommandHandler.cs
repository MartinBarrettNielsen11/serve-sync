using System.Runtime.CompilerServices;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.CreateTrainerProfile;

namespace UserAdministrationService.Application.Profiles.CreateInstructorProfile;

internal sealed class CreateInstructorProfileCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
    internal async ValueTask<Result> Handle(CreateInstructorProfileCommand command, CancellationToken cancellationToken)
#pragma warning restore CA1822
    {


        // retrieve user via repo an use Instructor extension method for the entity
        /*
        if (!club.HasInstructor(command.InstructorId))
        {
            return Error.NotFound(description: "Instructor not found");
        }
        */
    }
}