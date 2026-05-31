using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

internal record PlayerProfileCreatedEvent(Guid UserId, Guid ParticipantId) : IDomainEvent;