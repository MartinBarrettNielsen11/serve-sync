using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;
using SessionBookingService.Tests.Unit.Domain.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace SessionBookingService.Tests.Unit.Domain.PlayerAggregate;

public class PlayerTests
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
		Player player = PlayerFactory.Create();

		Session session1 = SessionFactory.CreateSession(
			date: SessionConstants.Date,
			timeRange: TimeSlotFactory.Create(startHourSession1, endHourSession1),
			id: Guid.NewGuid());

		Session session2 = SessionFactory.CreateSession(
			date: SessionConstants.Date,
			timeRange: TimeSlotFactory.Create(startHourSession2, endHourSession2),
			id: Guid.CreateVersion7());

		// Act
		Result<bool> addSession1Result = player.AddToSchedule(session1);
		Result<bool> addSession2Result = player.AddToSchedule(session2);

		// Assert
		Assert.False(addSession1Result.IsFailure);
		Assert.True(addSession2Result.IsFailure);
		// this error is not being throw - better change that.
		Assert.Equal(addSession2Result.Error, PlayerErrors.CannotHaveTwoOrMoreOverlappingSessions);
	}
}