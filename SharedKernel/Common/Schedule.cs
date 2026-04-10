using SharedKernel.Common;
using SharedKernel.Results;

namespace SharedKernel;

public sealed class Schedule : Entity
{ 
    private readonly Dictionary<DateOnly, List<TimeSlot>> _calendar;
    
    public Schedule(IDictionary<DateTime, List<TimeSlot>>? calendar = null,
                    Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        _calendar = (Dictionary<DateOnly, List<TimeSlot>>?)calendar ?? new Dictionary<DateOnly, List<TimeSlot>>();
    }

    public static Schedule Empty() => new(calendar: null, id: Guid.CreateVersion7());

    public Result BookTimeSlot(DateOnly date, TimeSlot time)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        var entryExists = _calendar.TryGetValue(date, out List<TimeSlot> timeSlots);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        
        if (!entryExists)
        {
            _calendar[date] = [time];
            return Result.Success();
        }
        
        if (timeSlots is not null &&
            timeSlots.Exists(ts => ts.IsOverlappingWith(time)))
        {
            return Result.Failure(Error.Failure(code: "no good", description: "dunno"));
        }
        
        timeSlots!.Add(time);
        
        return Result.Success();
    }

    public Result RemoveBooking(DateOnly date, TimeSlot time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeSlot>? timeSlots) || !timeSlots.Contains(time))
        {
            return Result.Failure(Error.NotFound(code: "", description: ""));
        }
        
        return Result.Success();
    }
    

}
