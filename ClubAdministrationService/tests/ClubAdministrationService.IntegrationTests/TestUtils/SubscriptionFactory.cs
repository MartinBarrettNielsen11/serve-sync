using ClubAdministrationService.Domain.SubscriptionAggregate;
using SubscriptionType = ClubAdministrationService.Domain.SubscriptionAggregate.SubscriptionType;

namespace ClubAdministrationService.IntegrationTests.TestUtils;

internal static class SubscriptionFactory
{
    internal static Subscription Create(SubscriptionType? subscriptionType = null,
        Guid? id = null)
    {
        return new Subscription(subscriptionType: subscriptionType ??  SubscriptionType.Free, 
            id ?? Guid.CreateVersion7());
    }
}