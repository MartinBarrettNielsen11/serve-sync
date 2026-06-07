using SharedKernel;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal sealed class TestDateTimeProvider(DateTime? fixedDateTime = null) : IDateTimeProvider
{
    public DateTime UtcNow => fixedDateTime ?? DateTime.UtcNow;
}