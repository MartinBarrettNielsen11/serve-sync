using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using SharedKernel.Results;
using Xunit;

namespace ClubAdministrationService.Tests.Unit.Domain.ClubAggregate;

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
		Result<bool> addCourtResult1 = club.AddCourt(court1);
		Result<bool> addCourtResult2 = club.AddCourt(court2);

		// missing an assert here
		Assert.False(addCourtResult1.IsFailure);
		Assert.True(addCourtResult2.IsFailure);
		Assert.Equal(addCourtResult2.Error, ClubErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
	}


	[Fact]
	public void AddCourt_WhenSameCourtIsAddedTwice_ShouldFail()
	{
		// Arrange
		Club club = ClubFactory.Create();
		Court court1 = CourtFactory.Create();

		// Act
		Result<bool> addCourtResult1 = club.AddCourt(court1);
		Result<bool> addCourtResult2 = club.AddCourt(court1);

		// missing an assert here
		Assert.False(addCourtResult1.IsFailure);
		Assert.True(addCourtResult2.IsFailure);
		Assert.Equal(addCourtResult2.Error, ClubErrors.CourtAlreadyExistsInClub);
	}
}