namespace ServeSync.Domain.ClubAggregate;

public class Club
{
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _courtIds = new();
    private readonly int _maxCourtCapacity;
    
    public Guid Id { get; }

    public Club(
        Guid subscriptionId,
        int maxCourtCapacity,
        Guid id)
    {
        _subscriptionId = subscriptionId;
        _maxCourtCapacity = maxCourtCapacity;
        Id = id;
    }
    
}
