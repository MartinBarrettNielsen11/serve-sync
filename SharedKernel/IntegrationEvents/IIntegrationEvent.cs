using System.Text.Json.Serialization;
using Mediator;
using SharedKernel.IntegrationEvents.ClubManagement;
using SharedKernel.IntegrationEvents.UserManagement;

namespace SharedKernel.IntegrationEvents;

[JsonDerivedType(typeof(AdminProfileCreatedIntegrationEvent), nameof(AdminProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(PlayerProfileCreatedIntegrationEvent), nameof(PlayerProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(InstructorProfileCreatedIntegrationEvent), nameof(InstructorProfileCreatedIntegrationEvent))]
[JsonDerivedType(typeof(SessionScheduledIntegrationEvent), nameof(SessionScheduledIntegrationEvent))]
[JsonDerivedType(typeof(CourtAddedIntegrationEvent), nameof(CourtAddedIntegrationEvent))]
[JsonDerivedType(typeof(CourtRemovedIntegrationEvent), nameof(CourtRemovedIntegrationEvent))]
public interface IIntegrationEvent : INotification
{
}