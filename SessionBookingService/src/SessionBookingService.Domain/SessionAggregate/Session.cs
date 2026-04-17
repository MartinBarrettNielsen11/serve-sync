using System;
using System.Collections.Generic;
using System.Linq;
using SessionBookingService.Domain.PlayerAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed class Session : RootAggregate
{
    public Guid _instructorId { get; }
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

    internal Result CancelBooking(Guid playerId, IDateTimeProvider provider)
    {
        if (IsTooCloseToSession(provider.UtcNow))
        {
            return Result.Failure(SessionErrors.CannotCancelBookingTooCloseToSession);
        }
        
        if (IsPastSession(provider.UtcNow))
        {
            return Result.Failure(SessionErrors.CannotCancelPastSession);
        }

        Booking booking = _bookings.First(b => b.PlayerId == playerId);
        
        _bookings.Remove(booking);
        
        return Result.Success();
    }

    public Result BookSpot(Player player)
    {
        if (_bookings.Count >= MaxPlayerCapacity)
        {
            return Result.Failure(SessionErrors.CannotHaveMoreBookingsThanPlayers);
        }
        
        Booking booking = new Booking(playerId: player.Id);

        // add some events and such here
        _bookings.Add(booking);

        return Result.Success();
    }
    
    private bool IsPastSession(DateTime utcNow)
    {
        return (Date.ToDateTime(Time.End) - utcNow).TotalHours < 0;
    }
    
    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int MinHours = 24;

        var timeDifference = (Date.ToDateTime(Time.Start) - utcNow).TotalHours;

        var exceedsLimit = timeDifference < MinHours;

        return exceedsLimit;
    }
}
