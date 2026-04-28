using System.Runtime.CompilerServices;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreateAdminProfile;

internal sealed class CreateAdminProfileCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    internal async ValueTask<Result> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
    {
        // retrieve user via repo an use Admin extension method for the entity
        /*
        if (!club.HasInstructor(command.InstructorId))
        {
            return Error.NotFound(description: "Instructor not found");
        }
        */
        return null!;
    }
}