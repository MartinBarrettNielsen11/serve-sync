using SharedKernel;
using SharedKernel.Common;

namespace ServeSync.Domain.SessionAggregate;

internal sealed class Booking : Entity
{
    public Guid PlayerId { get; }

    internal Booking(Guid playerId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        PlayerId = playerId;
    }
}
