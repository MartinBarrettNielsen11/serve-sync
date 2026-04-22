using System.Runtime.CompilerServices;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreatePlayerProfile;

internal sealed class CreatePlayerProfileCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
    internal async ValueTask<Result> Handle(CreatePlayerProfileCommand command, CancellationToken cancellationToken)
#pragma warning restore CA1822
    {


        // retrieve user via repo an use Player extension method for the entity
        /*
        if (!club.HasInstructor(command.InstructorId))
        {
            return Error.NotFound(description: "Instructor not found");
        }
        */
    }
}