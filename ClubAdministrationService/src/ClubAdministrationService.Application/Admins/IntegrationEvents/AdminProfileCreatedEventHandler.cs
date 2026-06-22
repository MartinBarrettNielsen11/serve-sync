using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using MediatR;
using SharedKernel.IntegrationEvents.UserManagement;

namespace ClubAdministrationService.Application.Admins.IntegrationEvents;

internal sealed class AdminProfileCreatedEventHandler(IAdminsRepository adminsRepository) : INotificationHandler<AdminProfileCreatedIntegrationEvent>
{
    public async Task Handle(AdminProfileCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Admin admin = new(userId: notification.UserId, id: notification.AdminId);

        await adminsRepository.AddAdminAsync(admin, cancellationToken);
    }
}
