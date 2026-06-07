using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.TestConstants;

namespace ClubAdministrationService.Tests.Unit.Factories;

internal static class SubscriptionFactory
{
    internal static Subscription Create(SubscriptionType? subscriptionType = null,
                                        Guid? id = null)
    {
        return new Subscription(subscriptionType: subscriptionType ?? SubscriptionConstants.DefaultSubscriptionType, 
                                id ?? SubscriptionConstants.Id);
    }
}