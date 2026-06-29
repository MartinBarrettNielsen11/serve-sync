using System.Text.Json.Serialization;
using Mediator;
using SharedKernel.IntegrationEvents.ClubManagement;
using SharedKernel.IntegrationEvents.UserManagement;

namespace SharedKernel.IntegrationEvents;

[JsonDerivedType(derivedType: typeof(AdminProfileCreatedIntegrationEvent), typeDiscriminator: nameof(AdminProfileCreatedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(PlayerProfileCreatedIntegrationEvent), typeDiscriminator: nameof(PlayerProfileCreatedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(InstructorProfileCreatedIntegrationEvent), typeDiscriminator: nameof(InstructorProfileCreatedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(SessionScheduledIntegrationEvent), typeDiscriminator: nameof(SessionScheduledIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(CourtAddedIntegrationEvent), typeDiscriminator: nameof(CourtAddedIntegrationEvent))]
[JsonDerivedType(derivedType: typeof(CourtRemovedIntegrationEvent), typeDiscriminator: nameof(CourtRemovedIntegrationEvent))]
public interface IIntegrationEvent : INotification { }