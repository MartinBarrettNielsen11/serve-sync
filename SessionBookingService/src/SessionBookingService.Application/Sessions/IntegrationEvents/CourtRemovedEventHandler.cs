using Mediator;
using SessionBookingService.Application.Common;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace SessionBookingService.Application.Sessions.IntegrationEvents;

internal sealed class CourtRemovedEventHandler(ISessionsRepository sessionsRepository) : INotificationHandler<CourtRemovedIntegrationEvent>
{
	public async ValueTask Handle(CourtRemovedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		var sessions = []; //don't keep ths - we must fetch by courtId

		sessions.ForEach(s => s.Cancel());

		// use repo for batched deletion - preferrably
	}
}
