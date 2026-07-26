namespace UserAdministrationService.Infrastructure.IntegrationEvents;

internal sealed record OutboxIntegrationEvent(string EventName, string EventContent);
