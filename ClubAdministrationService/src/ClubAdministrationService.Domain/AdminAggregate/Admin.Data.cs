using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Domain.AdminAggregate;

internal sealed partial class Admin
{
	internal void SetSubscription(Subscription subscription)
	{
		if (SubscriptionId is not null)
		{
			throw new InvalidOperationException();
		}

		SubscriptionId = subscription.Id;

		// add domain event
	}
}
