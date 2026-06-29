using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;

namespace SessionBookingService.Domain.CourtsAggregate.Events;

internal sealed record SessionScheduledForCourtEvent(Court Court, Session Session) : IDomainEvent;
