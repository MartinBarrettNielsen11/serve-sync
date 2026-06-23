using System.Text.Json.Serialization;
using Mediator;
using SharedKernel.IntegrationEvents.UserManagement;

namespace SharedKernel.IntegrationEvents;

[JsonDerivedType(typeof(AdminProfileCreatedIntegrationEvent), typeDiscriminator: nameof(AdminProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(PlayerProfileCreatedIntegrationEvent), typeDiscriminator: nameof(PlayerProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(InstructorProfileCreatedIntegrationEvent), typeDiscriminator: nameof(InstructorProfileCreatedIntegrationEvent))]
public interface IIntegrationEvent : INotification { }