using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

internal static class ClubErrors
{
    internal static readonly Error NumberOfCourtsCannotExceedSubscriptionLimit = Error.Failure(
        "Court.CannotHaveMoreCourtsThanSubscriptionAllows",
        "A club cannot have more courts than the subscription allows");
}