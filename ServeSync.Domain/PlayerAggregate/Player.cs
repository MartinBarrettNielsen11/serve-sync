using ServeSync.Domain.ScheduleAggregate;
using ServeSync.Domain.SessionAggregate;
using SharedKernel.Results;

namespace ServeSync.Domain.PlayerAggregate;

public class Player
{
    //private readonly Guid _userId;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();   
    
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