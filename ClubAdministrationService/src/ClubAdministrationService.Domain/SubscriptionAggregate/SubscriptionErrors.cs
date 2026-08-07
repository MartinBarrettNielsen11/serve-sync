using SharedKernel.Results;

namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal static class SubscriptionErrors
{
	internal static readonly Error NumberOfCourtsCannotExceedSubscriptionLimit =
		Error.Failure("Subscription.NumberOfCourtsCannotExceedSubscriptionLimit",
					"A subscription cannot have more courts than the subscription allows");
}
