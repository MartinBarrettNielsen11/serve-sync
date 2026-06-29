using System.Text.Json.Serialization;
using Mediator;
using SharedKernel.IntegrationEvents.UserManagement;

namespace SharedKernel.IntegrationEvents;

[JsonDerivedType(derivedType: typeof(AdminProfileCreatedIntegrationEvent), typeDiscriminator: nameof(AdminProfileCreatedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(PlayerProfileCreatedIntegrationEvent), typeDiscriminator: nameof(PlayerProfileCreatedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(InstructorProfileCreatedIntegrationEvent), typeDiscriminator: nameof(InstructorProfileCreatedIntegrationEvent))]
public interface IIntegrationEvent : INotification { }