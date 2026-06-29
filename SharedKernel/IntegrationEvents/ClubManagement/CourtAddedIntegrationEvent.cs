namespace SharedKernel.IntegrationEvents.ClubManagement;

#pragma warning disable MSG0005
public sealed record CourtAddedIntegrationEvent(string Name, Guid CourtId, Guid ClubId, int MaxDailySessions) : IIntegrationEvent;
#pragma warning restore MSG0005

