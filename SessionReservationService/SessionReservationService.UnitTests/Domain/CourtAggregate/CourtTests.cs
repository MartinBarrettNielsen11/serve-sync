using SessionReservationService.Domain.CourtAggregate;
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
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());
        
        var scheduleSession1Result = court.ScheduleSession(session1.Id);
        var scheduleSession2Result = court.ScheduleSession(session2.Id);
        
        Assert.Equal(scheduleSession2Result.Error, CourtErrors.NumberOfSessionsCannotExceedSubscriptionLimit);
    }
}