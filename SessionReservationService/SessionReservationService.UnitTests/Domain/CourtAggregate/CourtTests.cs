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
    
    
    [Theory]
    [InlineData(1, 3, 1, 3)] // exact overlap
    [InlineData(1, 3, 2, 3)] // second session inside first session
    [InlineData(1, 3, 2, 4)] // second session ends after session, but overlaps
    [InlineData(1, 3, 0, 2)] // second session starts before second session, but overlaps
    public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail(int startHourSession1,
                                                                                 int endHourSession1,
                                                                                 int startHourSession2,
                                                                                 int endHourSession2)
    {
        
        // Arrange
        var court = CourtFactory.CreateCourt(name: "yo", maxDailySessions: 2);
        
        var session1 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeRangeFactory.Create(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeRangeFactory.Create(startHourSession2, endHourSession2),
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