using SharedKernel;

namespace SessionBookingService.Domain.InstructorAggregate;

internal sealed partial class Instructor : RootAggregate
{
	private readonly Schedule _schedule = Schedule.Empty();
	private readonly List<Guid> _sessionIds = [];

	internal Instructor(Guid userId,
		Schedule? sch = null,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		UserId = userId;
		_schedule = sch ?? _schedule;
	}

	private Instructor()
	{
	} // For EF / serialization

	internal Guid UserId { get; }
}