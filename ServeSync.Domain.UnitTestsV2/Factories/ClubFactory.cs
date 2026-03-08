/*
using ServeSync.Domain.ClubAggregate;
using ServeSync.Domain.UnitTestsV2.Constants;

namespace ServeSync.Domain.UnitTestsV2.Factories;

internal sealed class ClubFactory
{
    internal static Club CreateClub(int maxCourtCapacity = SubscriptionConstants.MaxRoomsFreeTier,
                                    Guid? id = null)
    {
        return new Club(subscriptionId: SubscriptionConstants.Id,
                        maxCourtCapacity,
                        id: id ?? ClubConstants.Id);
    }
}
*/