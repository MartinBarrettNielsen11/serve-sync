using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

internal sealed record SessionSpotBookedEvent(Session Session, Booking Booking) : IDomainEvent;
