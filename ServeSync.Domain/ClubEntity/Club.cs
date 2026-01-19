namespace ServeSync.Domain.ClubEntity;

public class Club
{
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _courtIds = new();
    
    public Guid Id { get; }

    public Club(
        Guid subscriptionId,
        Guid id)
    {
        _subscriptionId = subscriptionId;
        Id = id;
    }
}
