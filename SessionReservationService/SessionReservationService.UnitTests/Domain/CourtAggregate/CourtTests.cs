using SessionReservationService.UnitTests.Domain.TestUtils;
using Xunit;

namespace SessionReservationService.UnitTests.Domain.CourtAggregate;

public class CourtTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var court = CourtFactory.CreateCourt(name: "yo", maxDailySessions: 1);
        
        Assert.Equal("yo", court.Name);
    }
}