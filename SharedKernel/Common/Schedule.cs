using SharedKernel.Common;
using SharedKernel.Common.ValueObjects;
using SharedKernel.Results;

namespace SharedKernel;

internal sealed class Schedule : Entity
{ 
    private readonly Dictionary<DateTime, List<TimeRange>> _calendar;

    public Schedule(IDictionary<DateTime, List<TimeRange>>? calendar = null,
                    Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _calendar = (Dictionary<DateTime, List<TimeRange>>?)calendar ?? new Dictionary<DateTime, List<TimeRange>>();
    }

    public static Schedule Empty() => new(calendar: null, id: Guid.NewGuid());

    internal Result BookTimeSlot(DateTime dateTime, TimeRange time)
    {
        var entryExists = _calendar.TryGetValue(dateTime, out List<TimeRange> timeSlots);
        
        if (!entryExists)
        {
            _calendar[dateTime] = [time];
            return Result.Success();
        }
        
        /*
         if (timeSlots!.Any(ts => ts.OverlapsWith(time)))
        {
            return Result.Failure(Error.Failure(code: "no good", description: "dunno"));
        }
        */
        
        timeSlots!.Add(time);
        
        return Result.Success();
    }

    internal Result RemoveBooking(DateTime dateTime, TimeRange time)
    {
        if (!_calendar.TryGetValue(dateTime, out List<TimeRange>? timeSlots) || !timeSlots.Contains(time))
        {
            return Result.Failure(Error.NotFound(code: "", description: ""));
        }
        
        return Result.Success();
    }
    

}
