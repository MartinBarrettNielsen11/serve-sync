using Ardalis.SmartEnum;

namespace SessionBookingService.Domain.SessionAggregate;

public sealed class SessionCategory(string name, int value) : SmartEnum<SessionCategory>(name, value)
{
	public static readonly SessionCategory Training = new(nameof(Training), 0);
	public static readonly SessionCategory Contest = new(nameof(Contest), 1);
}
