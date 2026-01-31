using SharedKernel;
using SharedKernel.Results;

namespace ServeSync.Domain.ClubAggregate;

public class Club : RootAggregate
{
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _courtIds = new();
    private readonly int _maxCourtCapacity;
    
    public Club(
        Guid subscriptionId,
        int maxCourtCapacity,
        Guid? id = null) 
        : base(id ?? Guid.NewGuid())
    {
        _subscriptionId = subscriptionId;
        _maxCourtCapacity = maxCourtCapacity;
    }

    public Result AddRoom(Guid roomId)
    {
        if (_courtIds.Contains(roomId)) return Result.Failure(Error.Failure(code: "", description: "Room already exists in gym"));
        
        return Result.Success();
    }
}
