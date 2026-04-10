using SharedKernel.Common;
using SharedKernel.Results;

namespace SharedKernel;

public sealed class Schedule : Entity
{ 
    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar;
    
    public Schedule(IDictionary<DateTime, List<TimeRange>>? calendar = null,
                    Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        _calendar = (Dictionary<DateOnly, List<TimeRange>>?)calendar ?? new Dictionary<DateOnly, List<TimeRange>>();
    }

    public static Schedule Empty() => new(calendar: null, id: Guid.CreateVersion7());

    public Result BookTimeSlot(DateOnly date, TimeRange time)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        var entryExists = _calendar.TryGetValue(date, out List<TimeRange> timeSlots);
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

    public Result RemoveBooking(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange>? timeSlots) || !timeSlots.Contains(time))
        {
            return Result.Failure(Error.NotFound(code: "", description: ""));
        }
        
        return Result.Success();
    }
    

}
