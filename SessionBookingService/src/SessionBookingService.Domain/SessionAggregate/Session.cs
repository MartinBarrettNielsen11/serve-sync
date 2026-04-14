using SessionBookingService.Domain.PlayerAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed class Session : RootAggregate
{
    private readonly Guid _instructorId;
    private readonly List<Booking> _bookings = new();
    
    public int MaxPlayerCapacity { get;}
    public DateOnly Date { get; }
    public TimeSlot Time { get; }
    public string Name { get; } = null!;
    public string Description { get; } = null!;


    public Session(string name,
                   string description,
                   int maxPlayerCapacity,
                   Guid instructorId,
                   DateOnly date,
                   TimeSlot time,
                   Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        Description = description;
        _instructorId = instructorId;
        Date = date;
        Time = time;
        MaxPlayerCapacity = maxPlayerCapacity;
    }

    internal Result CancelReservation(Guid participantId, IDateTimeProvider provider)
    {
        if (IsTooCloseToSession(provider.UtcNow))
            return Result.Failure(SessionErrors.CannotCancelReservationTooCloseToSession);

        return Result.Success();
    }

    public Result BookSpot(Player player)
    {
        if (_bookings.Count >= MaxPlayerCapacity)
        {
            return SessionErrors.Something;
        }
        
        // ...
    }
    
    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int MinHours = 24;

        var timeDifference = (Date.ToDateTime(Time.Start) - utcNow).TotalHours;

        var exceedsLimit = timeDifference < MinHours;

        return exceedsLimit;
    }
}
