using SharedKernel;

namespace SessionBookingService.Domain.PlayerAggregate;

internal sealed partial class Player
{
    public Guid UserId { get; }
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    public Player(Guid userId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }
    
    private Player() { } // For EF / serialization
}