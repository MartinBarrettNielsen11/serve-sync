using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.PlayerAggregate;

internal sealed class Player : RootAggregate
{
    public Guid UserId { get; }
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Player(Guid userId,
                  Schedule? schedule = null,
                  Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
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

        _sessionIds.Add(session.Id);
        return Result.Success();
    }
    
}
