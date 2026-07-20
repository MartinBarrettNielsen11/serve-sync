using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

internal sealed record BookingCanceledEvent(Session Session, Booking Booking) : IDomainEvent
{
	// add some eventual consistency errs here
}
