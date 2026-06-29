namespace SharedKernel.IntegrationEvents.ClubManagement;

#pragma warning disable MSG0005
public sealed record SessionScheduledIntegrationEvent(Guid RoomId, Guid InstructorId) : IIntegrationEvent;
#pragma warning restore MSG0005
