using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

internal sealed record SessionCanceledEvent(Session Session) : IDomainEvent;
