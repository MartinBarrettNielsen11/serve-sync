using SharedKernel;
using SharedKernel.Results;

namespace Domain1.InstructorAggregate;

internal sealed class Instructor : RootAggregate
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();

    internal Instructor(Guid userId, 
                        Schedule sch, 
                        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        _userId = userId;
        _schedule = sch ?? _schedule;
    }

    internal Result AddSessionToSchedule(Guid sessionId)
    {
        if (_sessionIds.Contains(sessionId))
            return Result.Failure(Error.Conflict(
                code: "", 
                description: "Session already exists in the schedule of the Instructor"));
        
        _sessionIds.Add(sessionId);
        return Result.Success();
    }
}
