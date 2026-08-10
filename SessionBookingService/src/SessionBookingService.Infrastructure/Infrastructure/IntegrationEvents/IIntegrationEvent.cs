using SharedKernel.IntegrationEvents;

namespace SessionBookingService.Infrastructure.Infrastructure.IntegrationEvents;

internal interface IIntegrationEventsPublisher
{
	void PublishEvent(IIntegrationEvent integrationEvent);
}
