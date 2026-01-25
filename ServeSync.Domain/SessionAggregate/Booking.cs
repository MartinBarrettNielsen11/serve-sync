using SharedKernel;

namespace ServeSync.Domain.SessionAggregate;

public class Booking : Entity
{
    private readonly Guid _participantId;
    
    public Booking(Guid participantId, Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _participantId = participantId;
    }

    public Guid ParticipantId => _participantId;
}