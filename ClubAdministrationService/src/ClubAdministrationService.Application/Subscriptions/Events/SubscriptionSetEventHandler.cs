

using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate.Events;
using Mediator;

namespace ClubAdministrationService.Application.Subscriptions.Events;

internal sealed class SubscriptionSetEventHandler(ISubscriptionsRepository subscriptionsRepository)
    : INotificationHandler<SubscriptionSetEvent>
{
    public async ValueTask Handle(SubscriptionSetEvent notification, CancellationToken cancellationToken)
    {
        await subscriptionsRepository.AddSubscriptionAsync(notification.Subscription, cancellationToken);
    }
}
