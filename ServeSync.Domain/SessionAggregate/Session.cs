using SharedKernel;

namespace ServeSync.Domain.SessionAggregate;

internal sealed class Session : RootAggregate
{
    private readonly Guid _instructorId;
    private readonly List<Booking> _bookings = new();
    private readonly int _maxPlayerCapacity;
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    internal Session(Guid instructorId,
                   DateOnly date,
                   TimeRange time,
                   int maxPlayerCapacity,
                   Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        _instructorId = instructorId;
        Date = date;
        Time = time;
        _maxPlayerCapacity = maxPlayerCapacity;
    }

    internal Result CancelReservation(Guid participantId, IDateTimeProvider provider)
    {
        if (IsTooCloseToSession(provider.UtcNow))
            return Result.Failure(SessionErrors.CannotCancelReservationTooCloseToSession);

        return Result.Success();
    }
    
    
    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int MinHours = 24;

        var timeDifference = (Date.ToDateTime(Time.Start) - utcNow).TotalHours;

        var exceedsLimit = timeDifference < MinHours;

        return exceedsLimit;
    }
}
