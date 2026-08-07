using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace SessionBookingService.Application.Courts.IntegrationEvents;

internal sealed class CourtAddedEventHandler(ICourtsRepository courtsRepository)
	: INotificationHandler<CourtAddedIntegrationEvent>
{
	public async ValueTask Handle(CourtAddedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		Court court = new(notification.Name,
						notification.MaxDailySessions,
						notification.ClubId,
						id: notification.CourtId);

		await courtsRepository.AddCourtAsync(court, cancellationToken);
	}
}
