using SharedKernel.Results;

namespace ServeSync.Domain.CourtAggregate;

public class CourtErrors
{
    public static readonly Error NumberOfSessionsCannotExceedSubscriptionLimit = Error.Failure(
        "Court.CannotHaveMoreSessionThanSubscriptionAllows",
        "A court cannot have more scheduled sessions than the subscription allows");
}