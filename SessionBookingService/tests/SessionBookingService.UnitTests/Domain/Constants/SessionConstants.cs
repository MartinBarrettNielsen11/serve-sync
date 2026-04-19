using System;
using SharedKernel;

namespace SessionBookingService.UnitTests.Domain.Constants;

internal static class SessionConstants
{
    internal const string Name = "Name";
    internal const string Description = "Description";
    internal static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
    internal static readonly TimeSlot Time = new(
        TimeOnly.MinValue.AddHours(8),
        TimeOnly.MinValue.AddHours(9));
    public const int MaxPlayerCapacity = 5;
}