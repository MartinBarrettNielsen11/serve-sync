using SharedKernel;

namespace ServeSync.Domain.SessionAggregate;

public class Session : Entity
{
    public Session(Guid? id = null) : base(id ?? Guid.NewGuid()) { }
    
    private readonly Guid _trainerId;
    private readonly List<Booking> _bookings = new();
    private readonly int _maxParticipants;
}
