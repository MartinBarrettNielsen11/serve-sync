using System.Text.Json;
using ClubAdministrationService.Domain.ClubAggregate.Events;
using Mediator;
using SharedKernel.IntegrationEvents;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace ClubAdministrationService.Infrastructure.IntegrationEvents.OutboxWriter;

internal sealed class OutboxWriterEventHandler(ClubDbContext clubDbContext)
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

	private async ValueTask AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
	{
		OutboxIntegrationEvent outboxIntegrationEvent = new(EventName: integrationEvent.GetType().Name,
															EventContent: JsonSerializer.Serialize(integrationEvent));
		await clubDbContext.OutboxIntegrationEvents.AddAsync(outboxIntegrationEvent);

		await clubDbContext.SaveChangesAsync();
	}
}
