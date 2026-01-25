using SharedKernel.Results;

namespace ServeSync.Domain.SessionAggregate;

public static class SessionErrors
{
    public static readonly Error CannotCancelPastSession = Error.Failure(
        "Session.CannotCancelPastSession",
        "A participant cannot cancel a booking for a session that has completed");
}