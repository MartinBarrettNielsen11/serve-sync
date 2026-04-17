using SharedKernel.Results;

namespace SessionBookingService.Domain.SessionAggregate;

internal static class SessionErrors
{
    internal static readonly Error CannotCancelPastSession = Error.Failure(
        "Session.CannotCancelPastSession",
        "A player cannot cancel a booking for a session that has completed");
    
    internal static readonly Error CannotHaveMoreBookingsThanPlayers = Error.Failure(
        code: "Session.CannotHaveMoreBookingsThanPlayers",
        description: "Cannot have more reservations than players");

    internal static readonly Error CannotCancelBookingTooCloseToSession = Error.Failure(
        code: "Session.CannotCancelBookingTooCloseToSession",
        description: "Cannot cancel reservation too close to session start time");
}