using SharedKernel.Results;

namespace SessionBookingService.Domain.SessionAggregate;

internal static class SessionErrors
{
	internal static readonly Error BookingNotFound = Error.NotFound(
		"Session.BookingNotFound",
		"Session booking not found");

	internal static readonly Error CannotCancelPastSession = Error.Failure(
		"Session.CannotCancelPastSession",
		"A player cannot cancel a booking for a session that has completed");

	internal static readonly Error CannotHaveMoreBookingsThanPlayers = Error.Failure(
		"Session.CannotHaveMoreBookingsThanPlayers",
		"Cannot have more reservations than players");

	internal static readonly Error CannotCancelBookingTooCloseToSession = Error.Failure(
		"Session.CannotCancelBookingTooCloseToSession",
		"Cannot cancel reservation too close to session start time");

	internal static readonly Error PlayerCannotReserveTwice = Error.Failure(
		"Session.PlayerCannotReserveTwice",
		"A player cannot reserve twice to the same session");
}
