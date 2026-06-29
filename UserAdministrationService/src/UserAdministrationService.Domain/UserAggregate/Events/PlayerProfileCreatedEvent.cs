using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

#pragma warning disable MSG0005
internal record PlayerProfileCreatedEvent(Guid UserId, Guid PlayerId) : IDomainEvent;
#pragma warning restore MSG0005
