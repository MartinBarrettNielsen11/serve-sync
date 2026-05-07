using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.UnitTests.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace ClubAdministrationService.UnitTests.Domain.SubscriptionAggregate;

public class SubscriptionTests
{
    [Fact]
    public void AddClub_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Subscription subscription = SubscriptionFactory.Create(subscriptionType: SubscriptionType.Pro);

        List<Club> gyms = Enumerable.Range(0, subscription.GetMaxClubsAllowed() + 1)
            .Select(_ => ClubFactory.Create(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<Result<bool>> addGymResults = gyms.ConvertAll(subscription.AddClub);

        // Assert
        IEnumerable<Result<bool>> allButLastAddGymResults = addGymResults.Take(..^1).ToList();
        Assert.True(allButLastAddGymResults.All(r => r.IsSuccess));
        
        Result<bool> lastAddGymResult = addGymResults[^1];
        Assert.True(lastAddGymResult.IsFailure);
        Assert.Equal(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit, lastAddGymResult.Error);
    }
}