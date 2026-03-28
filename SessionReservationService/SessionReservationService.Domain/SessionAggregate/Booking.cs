using SharedKernel.Common;

namespace SessionReservationService.Domain.SessionAggregate;

internal sealed class Booking : Entity
{
    public Guid PlayerId { get; }

    internal Booking(Guid playerId, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        PlayerId = playerId;
    }
}
