using ServeSync.Domain.Common;
using SharedKernel;

namespace ServeSync.Domain.ScheduleAggregate;

public class Schedule : Entity
{ 
    private readonly Guid _id;
    private readonly Dictionary<DateTime, List<TimeRange>> _calendar;

    public Schedule(Dictionary<DateTime, List<TimeRange>>? calendar,
                    Guid id) : base(id)
    {
        _calendar = calendar;
        _id = id;
    }

    public static Schedule Empty() => new Schedule(null, Guid.NewGuid());
}