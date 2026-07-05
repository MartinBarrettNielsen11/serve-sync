namespace SharedKernel.IntegrationEvents.ClubManagement;

public sealed record CourtAddedIntegrationEvent(string Name, Guid CourtId, Guid ClubId, int MaxDailySessions)
	: IIntegrationEvent;