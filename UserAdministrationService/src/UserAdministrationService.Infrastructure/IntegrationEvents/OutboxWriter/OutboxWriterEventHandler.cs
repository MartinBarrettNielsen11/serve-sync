using System.Text.Json;
using Mediator;
using SharedKernel.IntegrationEvents;
using SharedKernel.IntegrationEvents.UserManagement;
using UserAdministrationService.Domain.UserAggregate.Events;

namespace UserAdministrationService.Infrastructure.IntegrationEvents.OutboxWriter;

internal sealed class OutboxWriterEventHandler(UserDbContext userDbContext)
	: INotificationHandler<AdminProfileCreatedEvent>,
		INotificationHandler<PlayerProfileCreatedEvent>,
		INotificationHandler<InstructorProfileCreatedEvent>

{
	public async ValueTask Handle(AdminProfileCreatedEvent notification, CancellationToken cancellationToken)
	{
		AdminProfileCreatedIntegrationEvent integrationEvent = new(notification.UserId,
																	notification.AdminId);

		await AddOutboxIntegrationEventAsync(integrationEvent);
	}

	public async ValueTask Handle(InstructorProfileCreatedEvent notification, CancellationToken cancellationToken)
	{
		InstructorProfileCreatedIntegrationEvent integrationEvent = new(notification.UserId,
																		notification.InstructorId);
		await AddOutboxIntegrationEventAsync(integrationEvent);
	}

	public async ValueTask Handle(PlayerProfileCreatedEvent notification, CancellationToken cancellationToken)
	{
		PlayerProfileCreatedIntegrationEvent integrationEvent = new(notification.UserId,
																	notification.PlayerId);
		await AddOutboxIntegrationEventAsync(integrationEvent);
	}

	private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
	{
		OutboxIntegrationEvent outboxIntegrationEvent = new(integrationEvent.GetType().Name, JsonSerializer.Serialize(integrationEvent));
		await userDbContext.OutboxIntegrationEvents.AddAsync(outboxIntegrationEvent);
		await userDbContext.SaveChangesAsync();
	}
}
