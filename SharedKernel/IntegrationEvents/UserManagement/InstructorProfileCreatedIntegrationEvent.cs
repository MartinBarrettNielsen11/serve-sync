namespace SharedKernel.IntegrationEvents.UserManagement;

#pragma warning disable MSG0005
public sealed record InstructorProfileCreatedIntegrationEvent(Guid UserId, Guid InstructorId) : IIntegrationEvent;
#pragma warning restore MSG0005