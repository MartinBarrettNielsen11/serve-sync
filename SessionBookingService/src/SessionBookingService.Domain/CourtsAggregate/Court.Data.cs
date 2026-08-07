using Schedule = SessionBookingService.Domain.Common.Schedule;

namespace SessionBookingService.Domain.CourtsAggregate;

internal sealed partial class Court
{
	private readonly int _maxDailySessions;
	private readonly Schedule _schedule = Schedule.Empty();
	private readonly List<Guid> _sessionIds = new();

	public Court(
		string name,
		int maxDailySessions,
		Guid clubId,
		Schedule? schedule = null,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		Name = name;
		_maxDailySessions = maxDailySessions;
		ClubId = clubId;
		_schedule = schedule ?? Schedule.Empty();
	}

	private Court()
	{
	} // For EF / serialization

	public string Name { get; } = null!;
	public Guid ClubId { get; }
	public IReadOnlyList<Guid> SessionIds => _sessionIds.AsReadOnly();
}
