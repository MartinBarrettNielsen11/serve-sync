using SharedKernel.Results;
using SharedKernel.ValueObjects;

namespace SessionBookingService.Domain.Common;

public class TimeSlot(TimeOnly start, TimeOnly end) : ValueObject
{
	public TimeOnly Start { get; init; } = start;
	public TimeOnly End { get; init; } = end;

	public static Result<TimeSlot> FromDateTimes(DateTime start, DateTime end)
	{
		if (start.Date != end.Date || start >= end)
		{
			// Add Validation Err type and return that instead
			return Result.Failure<TimeSlot>(Error.Failure(code: "ValidationFailure", description: "ValidationFailure"));
		}

		return new TimeSlot(start: TimeOnly.FromDateTime(start),
							end: TimeOnly.FromDateTime(end));
	}

	public override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Start;
		yield return End;
	}

	public bool IsOverlappingWith(TimeSlot other)
	{
		if (Start >= other.End)
		{
			return false;
		}
		if (other.Start >= End)
		{
			return false;
		}

		return true;
	}
}
