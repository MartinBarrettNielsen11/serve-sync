using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace SessionBookingService.Application.Sessions.IntegrationEvents;

internal sealed class CourtRemovedEventHandler(ISessionsRepository sessionsRepository)
	: INotificationHandler<CourtRemovedIntegrationEvent>
{
	public async ValueTask Handle(CourtRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		List<Session> sessions = await sessionsRepository.ListByCourtId(notification.CourtId);

		sessions.ForEach(s => s.Cancel());

		await sessionsRepository.RemoveRangeAsync(sessions);
	}
}
