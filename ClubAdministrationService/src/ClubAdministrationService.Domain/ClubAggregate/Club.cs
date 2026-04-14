using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

internal sealed class Club : RootAggregate
{
    private readonly List<Guid> _courtIds = new();
    private readonly int _maxCourtCapacity;
    public string Name { get; } = null!;
    public Guid SubscriptionId { get; }

    
    public Club(
        string name,
        Guid subscriptionId,
        int maxCourtCapacity,
        Guid? id = null) 
        : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        SubscriptionId = subscriptionId;
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
