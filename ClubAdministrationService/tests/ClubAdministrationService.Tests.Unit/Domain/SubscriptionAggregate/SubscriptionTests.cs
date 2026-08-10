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
		Subscription subscription = SubscriptionFactory.Create(SubscriptionType.Starter);

		List<Club> clubs = Enumerable.Range(0, subscription.GetMaxCourtsAllowed() + 1)
									.Select(_ => ClubFactory.Create(id: Guid.NewGuid()))
									.ToList();

		// Act
		List<Result<bool>> addClubResults = clubs.ConvertAll(subscription.AddClub);

		// Assert
		IEnumerable<Result<bool>> allButLastAddClubResults = addClubResults.Take(..^1).ToList();
		Assert.True(allButLastAddClubResults.All(r => r.IsSuccess));

		Result<bool> lastAddClubResult = addClubResults[^1];
		Assert.True(lastAddClubResult.IsFailure);
		Assert.Equal(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit, lastAddClubResult.Error);
	}
}
