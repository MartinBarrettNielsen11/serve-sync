using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

internal sealed record AdminProfileCreatedEvent(Guid UserId, Guid AdminId) : IDomainEvent;