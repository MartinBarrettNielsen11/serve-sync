using ServeSync.Domain.Common;

namespace ServeSync.Domain.ScheduleEntity;

public class Schedule
{ 
    private readonly Guid _id;
    private readonly Dictionary<DateTime, List<TimeRange>> _calendar;

    public Schedule(Dictionary<DateTime, List<TimeRange>>? calendar,
                    Guid id)
    {
        _calendar = calendar;
        _id = id;
    }

    public static Schedule Empty() => new Schedule(null, Guid.NewGuid());
}