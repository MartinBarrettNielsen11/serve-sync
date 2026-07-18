using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed class Booking : Entity
{
	internal Booking(Guid playerId, Guid? id = null)
		: base(id ?? Guid.CreateVersion7())
	{
		PlayerId = playerId;
	}

	private Booking()
	{
	} // For EF / serialization

	public Guid PlayerId { get; }
}