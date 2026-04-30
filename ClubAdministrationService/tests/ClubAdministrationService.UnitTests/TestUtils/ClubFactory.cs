using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.UnitTests.Domain.TestConstants;

namespace ClubAdministrationService.UnitTests.TestUtils;

internal static class ClubFactory
{
    internal static Club Create(
        string name = ClubConstants.Name,
        int maxRooms = 5,
        Guid? id = null)
    {
        return new Club(
            name,
            maxRooms,
            subscriptionId: Guid.NewGuid(),
            id: id ?? ClubConstants.Id);
    }
}