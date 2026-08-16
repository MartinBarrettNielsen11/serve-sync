using SharedKernel;

namespace SessionBookingService.Infrastructure.Services;

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
	public DateTime UtcNow => DateTime.UtcNow;
}
