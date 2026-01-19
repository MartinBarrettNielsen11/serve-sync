using ServeSync.Domain.ScheduleEntity;

namespace ServeSync.Domain.PlayerEntity;

public class Player
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();   
}