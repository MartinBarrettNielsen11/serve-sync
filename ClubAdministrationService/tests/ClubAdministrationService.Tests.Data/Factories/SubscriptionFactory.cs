using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.TestConstants;
using SharedKernel.Results;

namespace ClubAdministrationService.Tests.Unit.Factories;

internal static class SubscriptionFactory
{
	internal static Subscription Create(SubscriptionType? subscriptionType = null,
										Guid? id = null)
	{
		return new Subscription(subscriptionType ?? SubscriptionConstants.DefaultSubscriptionType,
								id ?? SubscriptionConstants.Id);
	}


	internal static Subscription CreateWithClub(Club club, SubscriptionType? subscriptionType = null, Guid? id = null)
	{
		Subscription subscription = Create(subscriptionType, id);
		Result<bool> result = subscription.AddClub(club);
		if (result.IsFailure)
		{
			throw new InvalidOperationException($"Failed arranging test data: {result.Error.Description}");
		}

		return subscription;
	}


	internal static Subscription CreateWithClubs(IEnumerable<Club> clubs,
												SubscriptionType? subscriptionType = null,
												Guid? id = null)
	{
		Subscription subscription = Create(subscriptionType, id);

		foreach (Club club in clubs)
		{
			Result<bool> result = subscription.AddClub(club);

			if (result.IsFailure)
			{
				throw new
					InvalidOperationException($"Failed to add club: {club.Id} with error: {result.Error.Description}");
			}
		}

		return subscription;
	}
}
