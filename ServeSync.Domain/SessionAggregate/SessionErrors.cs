using SharedKernel.Results;

namespace ServeSync.Domain.SessionAggregate;

public static class SessionErrors
{
    public static readonly Error CannotCancelPastSession = Error.Failure(
        "Session.CannotCancelPastSession",
        "A player cannot cancel a booking for a session that has completed");
    
    public static readonly Error CannotHaveMoreReservationsThanPlayers = Error.Failure(
        code: "Session.CannotHaveMoreReservationsThanPlayers",
        description: "Cannot have more reservations than players");

    public static readonly Error CannotCancelReservationTooCloseToSession = Error.Failure(
        code: "Session.CannotCancelReservationTooCloseToSession",
        description: "Cannot cancel reservation too close to session start time");
}