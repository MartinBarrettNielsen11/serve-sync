using ServeSync.Domain.ScheduleAggregate;

namespace ServeSync.Domain.CourtAggregate;

public class Court
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Guid _clubId;
    private readonly Schedule _schedule = Schedule.Empty();
    
    public Guid Id { get; }

    public Court(
        int maxDailySessions,
        Guid clubId,
        Schedule schedule,
        Guid id)
    {
        _maxDailySessions = maxDailySessions;
        _clubId = clubId;
        _schedule = schedule;
        Id = id;
    }
    
}