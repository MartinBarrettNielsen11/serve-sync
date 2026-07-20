using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

internal sealed record BookingCanceledEvent(Session session, Booking booking) : IDomainEvent
{
	// add some eventual consistency errs here
}
