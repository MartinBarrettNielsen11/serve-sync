using SessionReservationService.Domain.InstructorAggregate;
using SessionReservationService.UnitTests.Domain.Constants;
using SessionReservationService.UnitTests.Domain.TestUtils;
using SharedKernel;
using Xunit;

namespace SessionReservationService.UnitTests.Domain.InstructorAggregate;

public class InstructorTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Instructor sut = InstructorFactory.Create();

        var session1 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeSlotFactory.Create(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: SessionConstants.Date,
            timeRange: TimeSlotFactory.Create(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var addSession1Result = sut.AddSessionToSchedule(session1);
        var addSession2Result = sut.AddSessionToSchedule(session2);

        // Assert
        Assert.True(addSession1Result.IsSuccess);
        Assert.True(addSession2Result.IsFailure);
        Assert.Equal(addSession2Result.Error, InstructorErrors.SessionCannotOverlap);
    }
}