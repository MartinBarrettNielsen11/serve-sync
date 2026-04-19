using System.Runtime.CompilerServices;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

// ReSharper disable once UnusedType.Global
internal sealed class DeleteCourtCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable CA1822
    internal async ValueTask<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
#pragma warning restore CA1822
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        

        /*
        if (!club.HasCourt(command.Court))
        {
            return Error.NotFound(description: "Room not found");
        }
        */
    }
}