using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.SubscriptionAggregate.Events;
using Mediator;

namespace ClubAdministrationService.Application.Clubs.Events;

internal sealed class ClubAddedToSubscriptionEventHandler(IClubsRepository clubsRepository)
	: INotificationHandler<ClubAddedToSubscriptionEvent>
{
	public async ValueTask Handle(ClubAddedToSubscriptionEvent notification, CancellationToken cancellationToken)
	{
		await clubsRepository.AddClubAsync(notification.Club, cancellationToken);
	}
}
