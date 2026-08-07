using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate.Events;
using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed partial class Subscription : RootAggregate
{
	internal Result<bool> AddClub(Club club)
	{
		if (_clubIds.Contains(club.Id))
		{
			return Result.Failure<bool>(Error.Failure("", "Club already exists"));
		}

		if (_maxCourtsAllowed <= _clubIds.Count)
		{
			return Result.Failure<bool>(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
		}

		_clubIds.Add(club.Id);

		DomainEvents.Add(new ClubAddedToSubscriptionEvent(this,
														club)); // consider usingsome fake puslisher or something - such that you can use that for your integation test setup and assert that an integartion event was raised
		return Result.Success(true);
	}

	internal int GetMaxCourtsAllowed()
	{
		return SubscriptionType.Name switch
		{
			nameof(SubscriptionType.Free) => 1,
			nameof(SubscriptionType.Starter) => 3,
			nameof(SubscriptionType.Pro) => int.MaxValue,
			_ => throw new InvalidOperationException()
		};
	}

	public int GetMaxDailySessionsAllowed()
	{
		return SubscriptionType.Name switch
		{
			nameof(SubscriptionType.Free) => 4,
			nameof(SubscriptionType.Starter) => int.MaxValue,
			nameof(SubscriptionType.Pro) => int.MaxValue,
			_ => throw new InvalidOperationException()
		};
	}

	public bool HasClub(Guid clubId)
	{
		return _clubIds.Contains(clubId);
	}
}
