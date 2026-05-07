namespace SharedKernel.IntegrationEvents.UserManagement;

internal sealed record InstructorProfileCreatedIntegrationEvent(Guid UserId, Guid InstructorId) : IIntegrationEvent;