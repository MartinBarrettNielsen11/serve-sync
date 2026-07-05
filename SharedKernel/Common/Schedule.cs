using SharedKernel.Common;
using SharedKernel.Results;

namespace SharedKernel;

public sealed class Schedule : Entity
{
	private readonly Dictionary<DateOnly, List<TimeSlot>> _calendar = [];

	public Schedule(
#pragma warning disable MA0016
#pragma warning disable S3427
		Dictionary<DateOnly, List<TimeSlot>>? calendar = null,
#pragma warning restore S3427
#pragma warning restore MA0016
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		_calendar = calendar ?? new Dictionary<DateOnly, List<TimeSlot>>();
	}

	private Schedule()
	{
	} // For EF / serialization

	public static Schedule Empty()
	{
		return new Schedule(null, Guid.CreateVersion7());
	}

	public Result<bool> BookTimeSlot(DateOnly date, TimeSlot time)
	{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		var entryExists = _calendar.TryGetValue(date, out List<TimeSlot> timeSlots);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

		if (!entryExists)
		{
			_calendar[date] = [time];
			return Result.Success(true);
		}

		if (timeSlots is not null &&
			timeSlots.Exists(ts => ts.IsOverlappingWith(time)))
			return Result.Failure<bool>(Error.Failure("no good", "dunno"));

		timeSlots!.Add(time);

		return Result.Success(true);
	}

	public Result<bool> RemoveBooking(DateOnly date, TimeSlot time)
	{
		if (!_calendar.TryGetValue(date, out List<TimeSlot>? timeSlots) || !timeSlots.Contains(time))
			return Result.Failure<bool>(Error.NotFound("", ""));

		return Result.Success(true);
	}
}