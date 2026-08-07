using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

internal static class ClubErrors
{
	internal static readonly Error NumberOfCourtsCannotExceedSubscriptionLimit =
		Error.Failure("Court.NumberOfCourtsCannotExceedSubscriptionLimit",
					"A club cannot have more courts than the subscription allows");

	internal static readonly Error CourtAlreadyExistsInClub = Error.Conflict("Court.CourtAlreadyExistsInClub",
																			"Court already exists in the club");
}
