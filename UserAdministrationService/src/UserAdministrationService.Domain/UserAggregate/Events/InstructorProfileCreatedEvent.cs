using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

#pragma warning disable MSG0005
internal sealed record InstructorProfileCreatedEvent(Guid UserId, Guid InstructorId) : IDomainEvent;
#pragma warning restore MSG0005
