namespace SharedKernel.IntegrationEvents.UserManagement;

#pragma warning disable MSG0005
public sealed record AdminProfileCreatedIntegrationEvent(Guid UserId, Guid AdminId) : IIntegrationEvent;
#pragma warning restore MSG0005
