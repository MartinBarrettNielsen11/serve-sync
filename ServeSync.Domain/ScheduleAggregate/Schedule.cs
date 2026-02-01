using ServeSync.Domain.Common;
using SharedKernel;

namespace ServeSync.Domain.ScheduleAggregate;

public class Schedule : Entity
{ 
    private readonly Dictionary<DateTime, List<TimeRange>> _calendar;

    public Schedule(IDictionary<DateTime, List<TimeRange>>? calendar = null,
                    Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _calendar = (Dictionary<DateTime, List<TimeRange>>?)calendar ?? new Dictionary<DateTime, List<TimeRange>>();
    }

    public static Schedule Empty() => new(calendar: null, id: Guid.NewGuid());

    public Result BookTimeSlot(DateTime dateTime, TimeRange time)
    {
        return Result.Success();
    }

    public Result RemoveBooking()
    {
        return Result.Success();
    }
}