using System.Text.Json;
using Mediator;
using SharedKernel.IntegrationEvents;
using SharedKernel.IntegrationEvents.UserManagement;
using UserAdministrationService.Domain.UserAggregate.Events;

namespace UserAdministrationService.Infrastructure.IntegrationEvents.OutboxWriter;

#pragma warning disable CA1711
internal sealed class OutboxWriterEventHandler(UserDbContext userDbContext)
#pragma warning restore CA1711
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

# pragma warning disable S1172
	private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
# pragma warning restore S1172
	{
		// Add interaction with dbContext for adding OutboxIntegrationEvents entry
		await userDbContext.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(integrationEvent.GetType().Name,
																						JsonSerializer
																							.Serialize(
																								integrationEvent)));

		await userDbContext.SaveChangesAsync();
	}
}
