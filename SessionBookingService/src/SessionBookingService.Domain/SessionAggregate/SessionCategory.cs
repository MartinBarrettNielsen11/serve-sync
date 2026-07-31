using Ardalis.SmartEnum;

namespace SessionBookingService.Domain.SessionAggregate;

public sealed class SessionCategory : SmartEnum<SessionCategory>
{
	public static readonly SessionCategory Training = new(nameof(Training), 0);
	public static readonly SessionCategory Contest = new(nameof(Contest), 1);

	public SessionCategory(string name, int value) : base(name, value)
	{
	}
}
