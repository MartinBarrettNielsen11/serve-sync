using SessionBookingService.Domain.Common;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Tests.Unit.Domain.Constants;

internal static class SessionConstants
{
	internal const string Name = "Name";
	internal const string Description = "Description";
	public const int MaxPlayerCapacity = 5;
	internal static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);

	internal static readonly TimeSlot Time = new(start: TimeOnly.MinValue.AddHours(8),
												 end: TimeOnly.MaxValue.AddHours(9));

	public static readonly List<SessionCategory> Categories = [];
}
