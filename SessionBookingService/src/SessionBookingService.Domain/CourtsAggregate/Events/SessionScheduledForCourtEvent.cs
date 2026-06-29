using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;

namespace SessionBookingService.Domain.CourtsAggregate.Events;

#pragma warning disable MSG0005
internal sealed record SessionScheduledForCourtEvent(Court Court, Session Session) : IDomainEvent;
#pragma warning restore MSG0005
