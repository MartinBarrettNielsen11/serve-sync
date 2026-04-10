using SessionReservationService.Domain.CourtAggregate;
using SessionReservationService.UnitTests.Domain.Constants;
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
        
        var scheduleSession1Result = court.ScheduleSession(session1);
        var scheduleSession2Result = court.ScheduleSession(session2);
        
        Assert.Equal(scheduleSession2Result.Error, CourtErrors.NumberOfSessionsCannotExceedSubscriptionLimit);
    }
    
    
    [Fact]
    public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail()
    {
        const int startHourSession1 = 1;
        const int endHourSession1 = 3;
        const int startHourSession2 = 1;
        const int endHourSession2 = 2;
        
        // Arrange
        var court = CourtFactory.CreateCourt(name: "yo", maxDailySessions: 2);
        
        var session1 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeRangeFactory.Create(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeRangeFactory.Create(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        // Act
        var scheduleSession1Result = court.ScheduleSession(session1);
        var scheduleSession2Result = court.ScheduleSession(session2);

        // Assert
        Assert.False(scheduleSession1Result.IsFailure);
        Assert.True(scheduleSession2Result.IsFailure);
        Assert.Equal(CourtErrors.SessionsCannotOverlap, scheduleSession2Result.Error);
    }
    
}