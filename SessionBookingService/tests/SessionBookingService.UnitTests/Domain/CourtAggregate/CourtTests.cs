using System;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.UnitTests.Domain.Constants;
using SessionBookingService.UnitTests.Domain.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace SessionBookingService.UnitTests.Domain.CourtAggregate;

public class CourtTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Court court = CourtFactory.CreateCourt(name: "yo", maxDailySessions: 1);
        Session session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        Session session2 = SessionFactory.CreateSession(id: Guid.NewGuid());
        
        _ = court.ScheduleSession(session1);
        Result scheduleSession2Result = court.ScheduleSession(session2);
        
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
        Court court = CourtFactory.CreateCourt(name: "yo", maxDailySessions: 2);
        
        Session session1 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeSlotFactory.Create(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeSlotFactory.Create(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        Result scheduleSession1Result = court.ScheduleSession(session1);
        Result scheduleSession2Result = court.ScheduleSession(session2);

        // Assert
        Assert.False(scheduleSession1Result.IsFailure);
        Assert.True(scheduleSession2Result.IsFailure);
        Assert.Equal(CourtErrors.SessionsCannotOverlap, scheduleSession2Result.Error);
    }
    
}