using ServeSync.Domain.ScheduleAggregate;
using ServeSync.Domain.SessionAggregate;
using SharedKernel;

namespace ServeSync.Domain.PlayerAggregate;

public sealed class Player : RootAggregate
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public Player(Guid userId,
                  Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
    }
    
    // intermediate placeholder for testing "Result"
    public Result AddToSchedule(Session session)
    {
        var intermediateCond = false; 
        
        if (intermediateCond)
        {
            return Result.Failure(
                Error.Conflict(
                    "Player.SessionAlreadyExists",
                    "Session already exists in player's schedule"));
        }

        return Result.Success();
    }

    
}
