using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;
using SessionBookingService.Tests.Unit.Domain.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace SessionBookingService.Tests.Unit.Domain.InstructorAggregate;

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

		Session session1 = SessionFactory.CreateSession(
			date: SessionConstants.Date,
			timeSlot: TimeSlotFactory.Create(startHourSession1, endHourSession1),
			id: Guid.CreateVersion7());

		Session session2 = SessionFactory.CreateSession(
			date: SessionConstants.Date,
			timeSlot: TimeSlotFactory.Create(startHourSession2, endHourSession2),
			id: Guid.CreateVersion7());

		// Act
		Result<bool> addSession1Result = sut.AddSessionToSchedule(session1);
		Result<bool> addSession2Result = sut.AddSessionToSchedule(session2);

		// Assert
		Assert.True(addSession1Result.IsSuccess);
		Assert.True(addSession2Result.IsFailure);
		Assert.Equal(addSession2Result.Error, InstructorErrors.SessionCannotOverlap);
	}
}
