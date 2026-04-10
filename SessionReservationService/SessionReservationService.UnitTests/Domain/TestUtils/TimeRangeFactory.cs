using SharedKernel;

namespace SessionReservationService.UnitTests.Domain.TestUtils;

public static class TimeRangeFactory
{
    public static TimeRange Create(int startHour, int endHour)
    {
        if (startHour is < 0 or > 23 || startHour >= endHour)
        {
            throw new Exception("Invalid startHour");
        }

        if (endHour is < 1 or > 24)
        {
            throw new Exception("invalid endHour");
        }

        return new TimeRange(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour)
        );
    }
}

