namespace SharedKernel.IntegrationEvents.UserManagement;

internal sealed record AdminProfileCreatedIntegrationEvent(Guid UserId, Guid AdminId) : IIntegrationEvent;