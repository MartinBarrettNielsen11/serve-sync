using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate.Events;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed partial class Session : RootAggregate
{
	public Result<bool> BookSpot(Player player)
	{
		if (_bookings.Count >= MaxPlayerCapacity)
		{
			return Result.Failure<bool>(SessionErrors.CannotHaveMoreBookingsThanPlayers);
		}

		Booking booking = new(player.Id);

		if (_bookings.Exists(b => b.PlayerId == booking.PlayerId))
		{
			return Result.Failure<bool>(SessionErrors.PlayerCannotReserveTwice);
		}

		_bookings.Add(booking);
		DomainEvents.Add(new SessionSpotBookedEvent(this, booking));

		return Result.Success(true);
	}

	internal Result<bool> CancelBooking(Guid playerId, IDateTimeProvider provider)
	{
		if (!_bookings.Exists(reservation => reservation.PlayerId == playerId))
		{
			return Result.Failure<bool>(SessionErrors.BookingNotFound);
		}

		if (IsTooCloseToSession(provider.UtcNow))
		{
			return Result.Failure<bool>(SessionErrors.CannotCancelBookingTooCloseToSession);
		}

		if (IsPastSession(provider.UtcNow))
		{
			return Result.Failure<bool>(SessionErrors.CannotCancelPastSession);
		}

		Booking booking = _bookings.First(b => b.PlayerId == playerId);

		_bookings.Remove(booking);
		DomainEvents.Add(new BookingCanceledEvent(this, booking));

		return Result.Success(true);
	}

	private bool IsPastSession(DateTime utcNow)
	{
		return (Date.ToDateTime(Time.End) - utcNow).TotalHours < 0;
	}

	private bool IsTooCloseToSession(DateTime utcNow)
	{
		const int MinHours = 24;

		var timeDifference = (Date.ToDateTime(Time.Start) - utcNow).TotalHours;

		var exceedsLimit = timeDifference < MinHours;

		return exceedsLimit;
	}

	public bool HasBookingForPlayer(Guid playerId)
	{
		return _bookings.Exists(b => b.PlayerId == playerId);
	}

	public void Cancel()
	{
		DomainEvents.Add(new SessionCanceledEvent(this));
	}
}
