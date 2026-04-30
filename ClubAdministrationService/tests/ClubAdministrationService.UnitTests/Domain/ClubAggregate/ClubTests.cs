using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.UnitTests.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace ClubAdministrationService.UnitTests.Domain.ClubAggregate;

public class ClubTests
{
    [Fact]
    public void AddCourt_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Club club = ClubFactory.Create(maxRooms: 1);
        Court court1 = CourtFactory.Create(id: Guid.CreateVersion7());
        Court court2 = CourtFactory.Create(id: Guid.CreateVersion7());

        // Act
        Result<bool> addRoom1Result =  club.AddCourt(court1.Id);
        Result<bool> addRoom2Result = club.AddCourt(court2.Id);
        
        // missing an assert here
    }
}