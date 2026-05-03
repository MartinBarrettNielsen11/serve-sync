using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateSession;

internal sealed class CreateSessionCommandHandler(ICourtsRepository courtsRepository)
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    internal async ValueTask<Result> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
    {
        Court? court = await courtsRepository.GetByIdAsync(command.CourtId);

        if (court is null)
        {
            return Result.Failure(Error.NotFound(code: "wut", description: "Court not found"));
        }
        
        return null!;
    }
}
