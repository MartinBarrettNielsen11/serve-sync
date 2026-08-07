namespace ClubAdministrationService.Infrastructure.IntegrationEvents;

public sealed record OutboxIntegrationEvent(string EventName, string EventContent);
