namespace ClubAdministrationService.Persistence.IntegrationEvents;

public sealed record OutboxIntegrationEvent(string EventName, string EventContent);