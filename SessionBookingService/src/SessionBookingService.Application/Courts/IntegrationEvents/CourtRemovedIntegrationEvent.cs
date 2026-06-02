using SharedKernel.IntegrationEvents;

namespace SessionBookingService.Application.Courts.IntegrationEvents;

internal sealed record CourtRemovedIntegrationEvent(Guid CourtId) : IIntegrationEvent;