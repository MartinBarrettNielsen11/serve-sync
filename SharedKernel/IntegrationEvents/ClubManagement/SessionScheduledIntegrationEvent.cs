namespace SharedKernel.IntegrationEvents.ClubManagement;

public sealed record SessionScheduledIntegrationEvent(Guid CourtId, Guid InstructorId) : IIntegrationEvent;
