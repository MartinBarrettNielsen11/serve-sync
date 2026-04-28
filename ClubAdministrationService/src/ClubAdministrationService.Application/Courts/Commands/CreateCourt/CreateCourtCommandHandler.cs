using System.Runtime.CompilerServices;
using ClubAdministrationService.Domain.CourtAggregate;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.CreateCourt;

internal sealed class CreateCourtCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable CA1822
    internal async ValueTask<Result<Court>> Handle(CreateCourtCommand command, CancellationToken cancellationToken)
#pragma warning restore CA1822
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        // ..
        return null!;
    }
}