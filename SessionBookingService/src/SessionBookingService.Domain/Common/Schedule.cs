using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.Common;

public sealed class Schedule : Entity
{
	private readonly Dictionary<DateOnly, List<TimeSlot>> _calendar = [];

	public Schedule(
		IReadOnlyDictionary<DateOnly, List<TimeSlot>> calendar,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		_calendar = calendar.ToDictionary(
			entry => entry.Key,
			entry => entry.Value.ToList());
	}

	private Schedule()
	{
	} // For EF / serialization

	public static Schedule Empty() => new(calendar: new Dictionary<DateOnly, List<TimeSlot>>(),
										  id: Guid.CreateVersion7());

	internal bool CanBookTimeSlot(DateOnly date, TimeSlot time)
	{
		if (!_calendar.TryGetValue(date, out List<TimeSlot>? timeSlots))
		{
			return true;
		}

		var timeSlotExists = timeSlots.Exists(ts => ts.IsOverlappingWith(time));

		return timeSlotExists;
	}

	public Result<bool> BookTimeSlot(DateOnly date, TimeSlot time)
	{
		var entryExists = _calendar.TryGetValue(date, out List<TimeSlot>? timeSlots);

		if (!entryExists)
		{
			_calendar[date] = [time];
			return Result.Success(true);
		}

		if (timeSlots is not null && timeSlots.Exists(ts => ts.IsOverlappingWith(time)))
		{
			return Result.Failure<bool>(Error.Conflict(code: "no good", description: "dunno"));
		}

		timeSlots!.Add(time);

		return Result.Success(value: true);
	}

	public Result<bool> RemoveBooking(DateOnly date, TimeSlot time)
	{
		if (!_calendar.TryGetValue(date, out List<TimeSlot>? timeSlots) || !timeSlots.Contains(time))
		{
			return Result.Failure<bool>(Error.NotFound("", ""));
		}

		return Result.Success(value: true);
	}
}
