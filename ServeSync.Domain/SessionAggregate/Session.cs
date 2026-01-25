namespace ServeSync.Domain.SessionAggregate;

public class Session
{
    private readonly Guid _trainerId;
    private readonly List<Booking> _bookings = new();
    private readonly int _maxParticipants;
}
