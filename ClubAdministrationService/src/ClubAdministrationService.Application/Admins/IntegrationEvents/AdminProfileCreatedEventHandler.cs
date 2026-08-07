using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using Mediator;
using SharedKernel.IntegrationEvents.UserManagement;

namespace ClubAdministrationService.Application.Admins.IntegrationEvents;

internal sealed class AdminProfileCreatedEventHandler(IAdminsRepository adminsRepository)
	: INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
	public async ValueTask Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
	{
		Admin admin = new(notification.UserId, id: notification.AdminId);

		await adminsRepository.AddAdminAsync(admin, cancellationToken);
	}
}
