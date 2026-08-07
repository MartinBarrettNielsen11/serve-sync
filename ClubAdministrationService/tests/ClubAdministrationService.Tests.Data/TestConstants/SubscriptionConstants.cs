using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Tests.Unit.TestConstants;

internal static class SubscriptionConstants
{
	internal const int MaxSessionsFreeTier = 3;
	internal const int MaxCourtsFreeTier = 1;
	internal const int MaxClubsFreeTier = 1;
	internal static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
	internal static readonly Guid Id = Guid.CreateVersion7();
}
