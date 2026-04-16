using System.Runtime.CompilerServices;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

internal sealed class DeleteCourtCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal sealed async ValueTask<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
    {
        

        /*
        if (!club.HasCourt(command.Court))
        {
            return Error.NotFound(description: "Room not found");
        }
        */
    }
}