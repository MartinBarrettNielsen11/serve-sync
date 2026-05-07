using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using MediatR;

namespace ClubAdministrationService.Application.Admins.IntegrationEvents;

internal sealed class AdminProfileCreatedEventHandler(IAdminsRepository adminsRepository) : INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
    public async Task Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Admin admin = new Admin(notification.UserId, id: notification.AdminId);

        await adminsRepository.AddAdminAsync(admin);
    }
}
