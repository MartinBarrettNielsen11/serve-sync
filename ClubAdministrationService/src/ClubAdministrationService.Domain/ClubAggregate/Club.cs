using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

internal sealed class Club : RootAggregate
{
    private readonly List<Guid> _courtIds = [];
    private readonly List<Guid> _instructorIds = [];
    
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

    internal Result<bool> AddCourt(Guid courtId)
    {
        if (_courtIds.Contains(courtId))
            return Result.Failure<bool>(Error.Failure(code: "", description: "Court already exists in Club"));
        
        if (_maxCourtCapacity < _courtIds.Count)
            return Result.Failure<bool>(ClubErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
        
        _courtIds.Add(courtId);
        
        return Result.Success<bool>(true);
    }
    
    internal bool HasCourt(Guid courtId) => _courtIds.Contains(courtId);
    
    internal Result<bool> AddTrainer(Guid trainerId)
    {
        if (_instructorIds.Contains(trainerId))
        {
            return Result.Failure<bool>(Error.Conflict(code: "", description: "Trainer already added to gym"));
        }

        _instructorIds.Add(trainerId);
        
        return Result.Success<bool>(true);
    }
    
    internal bool HasInstructor(Guid instructorId) => _instructorIds.Contains(instructorId);
    
    public void RemoveCourt(Guid courtId)
    {
        _courtIds.Remove(courtId);
        // add event regarding removal of court
    }

    private Club() { }
}
