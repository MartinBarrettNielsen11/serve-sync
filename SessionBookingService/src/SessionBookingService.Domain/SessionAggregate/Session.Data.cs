using TimeSlot = SessionBookingService.Domain.Common.TimeSlot;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed partial class Session
{
	private readonly List<Booking> _bookings = [];
	private readonly List<SessionCategory> _categories = [];

	internal Session(string name,
					string description,
					int maxPlayerCapacity,
					Guid instructorId,
					Guid courtId,
					DateOnly date,
					TimeSlot time,
					List<SessionCategory> categories,
					Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		Name = name;
		Description = description;
		InstructorId = instructorId;
		CourtId = courtId;
		Date = date;
		Time = time;
		MaxPlayerCapacity = maxPlayerCapacity;
		_categories = categories;
	}

	// Getting some odd error here
	private Session()
	{
	} // For EF / serialization

	public Guid InstructorId { get; }

	public int MaxPlayerCapacity { get; }
	public DateOnly Date { get; }
	public TimeSlot Time { get; } = null!;
	public string Name { get; } = null!;
	public string Description { get; } = null!;
	internal Guid CourtId { get; }

	public IReadOnlyList<SessionCategory> Categories => _categories;
	public int NumPlayers => _bookings.Count;
}
