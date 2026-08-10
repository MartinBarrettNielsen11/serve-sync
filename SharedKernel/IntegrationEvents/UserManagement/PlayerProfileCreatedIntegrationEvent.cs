namespace SharedKernel.IntegrationEvents.UserManagement;

public sealed record PlayerProfileCreatedIntegrationEvent(Guid UserId, Guid PlayerId) : IIntegrationEvent;
