namespace ServeSync.Domain.SessionAggregate;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Guid> participants = new();
    private readonly int _maxParticipants;
}
