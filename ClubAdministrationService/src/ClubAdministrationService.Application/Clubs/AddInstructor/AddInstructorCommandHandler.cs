using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Courts.Commands.DeleteCourt;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.AddInstructor;

// ReSharper disable once UnusedType.Global
internal sealed class AddInstructorCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    internal async ValueTask<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
    {

        /*
        if (!club.HasInstructor(command.InstructorId))
        {
            return Error.NotFound(description: "Instructor not found");
        }
        */
        return null!;
    }
}