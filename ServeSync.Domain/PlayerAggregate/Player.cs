using ServeSync.Domain.ScheduleAggregate;

namespace ServeSync.Domain.PlayerAggregate;

public class Player
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();   
}