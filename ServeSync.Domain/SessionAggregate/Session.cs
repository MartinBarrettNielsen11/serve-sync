using ServeSync.Domain.Common;
using SharedKernel;

namespace ServeSync.Domain.SessionAggregate;

public class Session : RootAggregate
{
    private readonly Guid _instructorId;
    private readonly List<Booking> _bookings = new();
    private readonly int _maxPlayerCapacity;
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    public Session(Guid instructorId,
                   DateOnly date,
                   TimeRange time,
                   int maxPlayerCapacity,
                   Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _instructorId = instructorId;
        Date = date;
        Time = time;
        _maxPlayerCapacity = maxPlayerCapacity;
    }
}
