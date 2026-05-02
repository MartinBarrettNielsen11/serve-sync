using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;

namespace SessionBookingService.Domain.CourtsAggregate.Events;

internal sealed record SessionScheduledForCourtEvent(Court court, Session session) : IDomainEvent;
