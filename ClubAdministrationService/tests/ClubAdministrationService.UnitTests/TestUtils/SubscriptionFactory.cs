using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.UnitTests.Domain.TestConstants;

namespace ClubAdministrationService.UnitTests.TestUtils;

internal static class SubscriptionFactory
{
    internal static Subscription Create(SubscriptionType? subscriptionType = null,
                                        Guid? id = null)
    {
        return new Subscription(subscriptionType: subscriptionType ?? SubscriptionConstants.DefaultSubscriptionType, 
                                id ?? SubscriptionConstants.Id);
    }
}