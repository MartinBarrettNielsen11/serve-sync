using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using SharedKernel.Results;
using Xunit;

namespace ClubAdministrationService.Tests.Unit.Domain.SubscriptionAggregate;

public sealed class SubscriptionTests
{
	[Fact]
	public void AddClub_WhenMoreThanSubscriptionAllows_ShouldFail()
	{
		// Arrange
		Subscription subscription = SubscriptionFactory.Create(SubscriptionType.Pro);

		List<Club> clubs = Enumerable.Range(0, subscription.GetMaxCourtsAllowed() + 1)
									.Select(_ => ClubFactory.Create(id: Guid.NewGuid()))
									.ToList();

		// Act
		List<Result<bool>> addGymResults = clubs.ConvertAll(subscription.AddClub);

		// Assert
		IEnumerable<Result<bool>> allButLastAddGymResults = addGymResults.Take(..^1).ToList();
		Assert.True(allButLastAddGymResults.All(r => r.IsSuccess));

		Result<bool> lastAddGymResult = addGymResults[^1];
		Assert.True(lastAddGymResult.IsFailure);
		Assert.Equal(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit, lastAddGymResult.Error);
	}
}
