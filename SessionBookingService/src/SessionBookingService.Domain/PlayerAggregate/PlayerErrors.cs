using SharedKernel.Results;

namespace SessionBookingService.Domain.PlayerAggregate;

internal static class PlayerErrors
{
	internal static readonly Error CannotHaveTwoOrMoreOverlappingSessions =
		Error.Failure(
			"Player.CannotHaveTwoOrMoreOverlappingSessions",
			"A player cannot have two or more overlapping sessions");
}
