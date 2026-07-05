using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

public sealed record PlayerProfileCreatedEvent(Guid UserId, Guid PlayerId) : IDomainEvent;