using SharedKernel;

namespace ServeSync.Domain.ClubAggregate;

internal sealed class Club : RootAggregate
{
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _courtIds = new();
    private readonly int _maxCourtCapacity;
    
    internal Club(
        Guid subscriptionId,
        int maxCourtCapacity,
        Guid? id = null) 
        : base(id ?? Guid.CreateVersion7())
    {
        _subscriptionId = subscriptionId;
        _maxCourtCapacity = maxCourtCapacity;
    }

    internal Result AddCourt(Guid courtId)
    {
        if (_courtIds.Contains(courtId))
            return Result.Failure(Error.Failure(code: "", description: "Court already exists in Club"));
        
        if (_maxCourtCapacity < _courtIds.Count)
            return Result.Failure(ClubErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
        
        _courtIds.Add(courtId);
        
        return Result.Success();
    }
}
