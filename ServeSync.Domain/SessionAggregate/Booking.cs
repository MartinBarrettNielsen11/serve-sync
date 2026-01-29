using SharedKernel;

namespace ServeSync.Domain.SessionAggregate;

public class Booking : Entity
{
    public Guid PlayerId { get; }

    public Booking(Guid playerId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        PlayerId = playerId;
    }
}