using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

internal sealed record InstructorProfileCreatedEvent(Guid UserId, Guid InstructorId) : IDomainEvent;