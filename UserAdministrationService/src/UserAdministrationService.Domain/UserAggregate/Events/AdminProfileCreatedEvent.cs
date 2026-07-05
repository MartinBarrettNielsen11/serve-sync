using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

public sealed record AdminProfileCreatedEvent(Guid UserId, Guid AdminId) : IDomainEvent;