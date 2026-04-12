using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

public static class ClubErrors
{
    public static readonly Error NumberOfCourtsCannotExceedSubscriptionLimit = Error.Failure(
        "Court.CannotHaveMoreCourtsThanSubscriptionAllows",
        "A club cannot have more courts than the subscription allows");
}