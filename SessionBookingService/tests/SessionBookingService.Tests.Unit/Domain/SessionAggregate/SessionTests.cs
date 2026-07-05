using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;
using SessionBookingService.Tests.Unit.Domain.TestUtils;
using SharedKernel.Results;
using Xunit;

namespace SessionBookingService.Tests.Unit.Domain.SessionAggregate;

public class SessionTests
{
	[Fact]
	public void BookSpot_WhenNoMoreRoom_ShouldFailReservation()
	{
		// Arrange
		Session session = SessionFactory.CreateSession(maxPlayerCapacity: 1);
		Player player = PlayerFactory.Create();

		// Act
		Result<bool> firstReservationResult = session.BookSpot(player);
		Result<bool> secondReservationResult = session.BookSpot(player);

		// Assert
		Assert.True(firstReservationResult.IsSuccess);
		Assert.True(secondReservationResult.IsFailure);
		Assert.Equal(secondReservationResult.Error, SessionErrors.CannotHaveMoreBookingsThanPlayers);
	}

	[Fact]
	public void CancelBooking_WhenCancellationTimeTooCloseToSession_ShouldFailCancellation()
	{
		// Arrange
		Session session = SessionFactory.CreateSession(date: SessionConstants.Date);
		Player player = PlayerFactory.Create();

		DateTime dateAndTimeOfCancellation = SessionConstants.Date.ToDateTime(TimeOnly.MinValue);

		// Act
		Result<bool> reservationResult = session.BookSpot(player);
		Result<bool> cancellationResult = session.CancelBooking(
			player.Id,
			new TestDateTimeProvider(dateAndTimeOfCancellation));

		// Assert
		Assert.True(reservationResult.IsSuccess);
		Assert.True(cancellationResult.IsFailure);
		Assert.Equal(cancellationResult.Error, SessionErrors.CannotCancelBookingTooCloseToSession);
	}
}