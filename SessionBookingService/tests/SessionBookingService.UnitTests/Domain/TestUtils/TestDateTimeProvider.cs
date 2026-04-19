using System;
using SharedKernel;

namespace SessionBookingService.UnitTests.Domain.TestUtils;

internal sealed class TestDateTimeProvider(DateTime? fixedDateTime = null) : IDateTimeProvider
{
    public DateTime UtcNow => fixedDateTime ?? DateTime.UtcNow;
}