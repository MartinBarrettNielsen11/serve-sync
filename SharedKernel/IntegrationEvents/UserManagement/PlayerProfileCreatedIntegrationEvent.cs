namespace SharedKernel.IntegrationEvents.UserManagement;

#pragma warning disable MSG0005
public sealed record PlayerProfileCreatedIntegrationEvent(Guid UserId, Guid PlayerId) : IIntegrationEvent;
#pragma warning restore MSG0005