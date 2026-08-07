using Schedule = SessionBookingService.Domain.Common.Schedule;

namespace SessionBookingService.Domain.PlayerAggregate;

internal sealed partial class Player
{
	private readonly Schedule _schedule = Schedule.Empty();
	private readonly List<Guid> _sessionIds = [];

	public Player(Guid userId,
		Schedule? schedule = null,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		UserId = userId;
		_schedule = schedule ?? Schedule.Empty();
	}

	private Player()
	{
	} // For EF / serialization

	public Guid UserId { get; }
	public IReadOnlyList<Guid> SessionIds => _sessionIds;
}
