using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

#pragma warning disable MSG0005
internal sealed record AdminProfileCreatedEvent(Guid UserId, Guid AdminId) : IDomainEvent;
#pragma warning restore MSG0005
