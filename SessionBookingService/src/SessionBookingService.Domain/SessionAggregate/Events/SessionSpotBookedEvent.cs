using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

internal sealed record SessionSpotBookedEvent(Session session, Booking booking) : IDomainEvent;
