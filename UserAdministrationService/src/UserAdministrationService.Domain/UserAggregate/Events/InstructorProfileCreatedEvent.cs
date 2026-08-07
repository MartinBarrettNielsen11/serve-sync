using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate.Events;

public sealed record InstructorProfileCreatedEvent(Guid UserId, Guid InstructorId) : IDomainEvent;
