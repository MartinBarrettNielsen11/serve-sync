using ServeSync.Domain.ScheduleAggregate;
using SharedKernel;

namespace ServeSync.Domain.InstructorAggregate;

public class Instructor : RootAggregate
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();

    public Instructor(Guid userId, 
                      Schedule sch, 
                      Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _schedule = sch ?? _schedule;
    }
}