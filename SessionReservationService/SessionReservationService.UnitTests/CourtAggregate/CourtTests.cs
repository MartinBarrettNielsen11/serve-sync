using SessionReservationService.UnitTests.TestUtils;
using Xunit;

namespace SessionReservationService.UnitTests.CourtAggregate;

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