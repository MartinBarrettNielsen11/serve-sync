using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate;

public static class CourtErrors
{
    public static readonly Error NumberOfSessionsCannotExceedSubscriptionLimit = Error.Failure(
        "Court.NumberOfSessionsCannotExceedSubscriptionLimit",
        "A court cannot have more scheduled sessions than the subscription allows");
    
    public static readonly Error SessionsCannotOverlap = Error.Failure(
        "Court.SessionsCannotOverlap",
        "A court cannot have two or more overlapping sessions");
}
