using SharedKernel.IntegrationEvents;

namespace SessionBookingService.Infrastructure.Infrastructure.IntegrationEventsPublisher;

internal interface IIntegrationEventsPublisher
{
	void PublishEvent(IIntegrationEvent integrationEvent);
}
