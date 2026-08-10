namespace SharedKernel.IntegrationEvents.UserManagement;

public sealed record InstructorProfileCreatedIntegrationEvent(Guid UserId, Guid InstructorId) : IIntegrationEvent;
