namespace SharedKernel.IntegrationEvents.UserManagement;

public sealed record AdminProfileCreatedIntegrationEvent(Guid UserId, Guid AdminId) : IIntegrationEvent;