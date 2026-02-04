using ServeSync.Domain.SessionAggregate;
using SharedKernel;

namespace ServeSync.Domain.PlayerAggregate;

internal sealed class Player : RootAggregate
{
    private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public Player(Guid userId,
                  Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        _userId = userId;
    }
    
    // intermediate placeholder for testing "Result"
    internal Result AddToSchedule(Session session)
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
