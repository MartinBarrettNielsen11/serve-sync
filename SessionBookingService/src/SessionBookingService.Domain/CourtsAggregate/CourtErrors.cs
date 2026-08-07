using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate;

internal static class CourtErrors
{
	internal static readonly Error NumberOfSessionsCannotExceedSubscriptionLimit =
		Error.Failure("Court.NumberOfSessionsCannotExceedSubscriptionLimit",
					"A court cannot have more scheduled sessions than the subscription allows");

	internal static readonly Error SessionsCannotOverlap = Error.Failure("Court.SessionsCannotOverlap",
																		"A court cannot have two or more overlapping sessions");
}
