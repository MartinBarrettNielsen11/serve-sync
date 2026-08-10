namespace SharedKernel.IntegrationEvents.ClubManagement;

public sealed record CourtRemovedIntegrationEvent(Guid CourtId) : IIntegrationEvent;
