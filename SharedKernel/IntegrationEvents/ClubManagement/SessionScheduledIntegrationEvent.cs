namespace SharedKernel.IntegrationEvents.ClubManagement;

public sealed record SessionScheduledIntegrationEvent(Guid RoomId, Guid InstructorId) : IIntegrationEvent;
