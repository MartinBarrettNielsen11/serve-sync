namespace SharedKernel.IntegrationEvents.ClubManagement;

#pragma warning disable MSG0005
public sealed record CourtRemovedIntegrationEvent(Guid CourtId) : IIntegrationEvent;
#pragma warning restore MSG0005