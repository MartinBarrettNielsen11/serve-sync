using ServeSync.Domain.ScheduleAggregate;
using SharedKernel;

namespace ServeSync.Domain.CourtAggregate;

public class Court : RootAggregate
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Guid _clubId;
    private readonly Schedule _schedule = Schedule.Empty();
    
    public Guid? Id { get; }

    public Court(
        int maxDailySessions,
        Guid clubId,
        Schedule schedule,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxDailySessions = maxDailySessions;
        _clubId = clubId;
        _schedule = schedule;
        Id = id;
    }
    
}