using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Courts.Commands.DeleteCourt;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.AddInstructor;

internal sealed class AddInstructorCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal async ValueTask<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
    {
        

        /*
        if (!club.HasInstructor(command.InstructorId))
        {
            return Error.NotFound(description: "Instructor not found");
        }
        */
    }
}