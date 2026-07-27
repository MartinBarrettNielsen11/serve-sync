using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace SessionBookingService.Application.Courts.IntegrationEvents;

internal sealed class CourtRemovedEventHandler(ICourtsRepository courtsRepository)
	: INotificationHandler<CourtRemovedIntegrationEvent>
{
	public async ValueTask Handle(CourtRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		Court? court = await courtsRepository.GetByIdAsync(notification.CourtId, cancellationToken);

		if (court is not null)
        {
            await courtsRepository.DeleteAsync(court, cancellationToken);
        }
    }
}
