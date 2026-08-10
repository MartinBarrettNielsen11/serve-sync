using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Tests.Unit.TestConstants;

namespace ClubAdministrationService.Tests.Unit.Factories;

internal static class ClubFactory
{
	internal static Club Create(
		string name = ClubConstants.Name,
		int maxCourtCapacity = 5,
		Guid? subscriptionId = null,
		Guid? id = null)
	{
		return new Club(name,
						maxCourtCapacity,
						subscriptionId ?? Guid.NewGuid(),
						id ?? ClubConstants.Id);
	}
}
