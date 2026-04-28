using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateBooking;

internal sealed class CreateBookingCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async ValueTask<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
    {
        return null!;
    }

}