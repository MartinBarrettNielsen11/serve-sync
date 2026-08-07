using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.Tests.Unit.TestConstants;

namespace ClubAdministrationService.Tests.Unit.Factories;

internal static class CourtFactory
{
	internal static Court Create(
		int maxDailySessions = CourtConstants.MaxDailySessions,
		Guid? gymId = null,
		Guid? id = null)
	{
		return new Court(CourtConstants.Name,
						maxDailySessions: maxDailySessions,
						clubId: gymId ?? ClubConstants.Id,
						id: id ?? CourtConstants.Id);
	}
}
