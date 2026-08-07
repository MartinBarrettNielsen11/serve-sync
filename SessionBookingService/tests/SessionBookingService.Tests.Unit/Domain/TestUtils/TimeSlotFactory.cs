using SessionBookingService.Domain.Common;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal static class TimeSlotFactory
{
	public static TimeSlot Create(int startHour, int endHour)
	{
		if (startHour is < 0 or > 23 || startHour >= endHour)
		{
			throw new ArgumentOutOfRangeException(nameof(startHour));
		}

		if (endHour is < 1 or > 24)
		{
			throw new ArgumentOutOfRangeException(nameof(endHour));
		}

		return new TimeSlot(TimeOnly.MinValue.AddHours(startHour),
							TimeOnly.MinValue.AddHours(endHour));
	}
}
