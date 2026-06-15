using SharedKernel.Entity;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed class Booking : Entity
{
    public Guid PlayerId { get; }

    internal Booking(Guid playerId, Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        PlayerId = playerId;
    }
    
    private Booking() { } // For EF / serialization
}
