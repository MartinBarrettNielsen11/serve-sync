namespace SharedKernel.IntegrationEvents.UserManagement;

internal sealed record PlayerProfileCreatedIntegrationEvent(Guid UserId, Guid PlayerId) : IIntegrationEvent;