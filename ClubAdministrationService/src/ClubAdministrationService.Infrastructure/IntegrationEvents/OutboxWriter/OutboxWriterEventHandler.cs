using ClubAdministrationService.Domain.ClubAggregate.Events;
using Mediator;
using SharedKernel.IntegrationEvents;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace ClubAdministrationService.Infrastructure.IntegrationEvents.OutboxWriter;

#pragma warning disable CA1711
internal sealed class OutboxWriterEventHandler(ClubDbContext clubDbContext)
#pragma warning restore CA1711
	: INotificationHandler<CourtAddedToClubEvent>, INotificationHandler<CourtRemovedFromClubEvent>

{
	public async ValueTask Handle(CourtAddedToClubEvent notification, CancellationToken cancellationToken)
	{
		CourtAddedIntegrationEvent integrationEvent = new(notification.Court.Name,
														notification.Court.Id,
														notification.Club.Id,
														notification.Court.MaxDailySessions);

		await AddOutboxIntegrationEventAsync(integrationEvent);
	}

	public async ValueTask Handle(CourtRemovedFromClubEvent notification, CancellationToken cancellationToken)
	{
		CourtRemovedIntegrationEvent integrationEvent = new(notification.CourtId);
		await AddOutboxIntegrationEventAsync(integrationEvent);
	}

#pragma warning disable S1172
	private async ValueTask AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
#pragma warning restore S1172
	{
		// Add interaction with dbContext for adding OutboxIntegrationEvents entry

		await clubDbContext.SaveChangesAsync();
	}
}
