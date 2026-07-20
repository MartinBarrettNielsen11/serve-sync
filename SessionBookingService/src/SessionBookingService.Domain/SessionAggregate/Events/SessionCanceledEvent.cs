using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate.Events;

public sealed record SessionCanceledEvent(Session Session) : IDomainEvent;
