using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace ClubAdministrationService.Application.Clubs.IntegrationEvents;

internal sealed class SessionScheduledEventHandler(IClubsRepository clubsRepository)
    : INotificationHandler<SessionScheduledIntegrationEvent>
{
    public async ValueTask Handle(SessionScheduledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Club? club = await clubsRepository.GetByIdAsync(notification.RoomId, cancellationToken);

        if (club is null)
        {
            throw new InvalidOperationException($"No club found with id {notification.RoomId}");
        }

        club.AddInstructor(notification.TrainerId);
    }
}
