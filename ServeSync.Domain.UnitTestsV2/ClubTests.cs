using ServeSync.Domain.ClubAggregate;
using ServeSync.Domain.UnitTestsV2.Factories;

namespace ServeSync.Domain.UnitTestsV2;

public class ClubTests
{
    [Fact]
    public void Given_MaxCapacityOfCourtsIsMetForSubscription_When_AddRoom_Then_Fail()
    {
        // Arrange
        Club club = ClubFactory.CreateClub(maxCourtCapacity: 1);
        // [Continue here]
    }
}
