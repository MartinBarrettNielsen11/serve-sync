using ServeSync.Domain.ScheduleAggregate;
using SharedKernel;

namespace ServeSync.Domain.InstructorAggregate;

public sealed class Instructor : RootAggregate
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

    public Result AddSessionToSchedule(Guid sessionId)
    {
        if (_sessionIds.Contains(sessionId))
            return Result.Failure(Error.Conflict(
                code: "", 
                description: "Session already exists in the schedule of the Instructor"));
        
        _sessionIds.Add(sessionId);
        return Result.Success();
    }
}
