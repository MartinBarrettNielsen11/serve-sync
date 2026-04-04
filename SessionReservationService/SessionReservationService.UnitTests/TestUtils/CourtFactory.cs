using SessionReservationService.Domain.CourtAggregate;

namespace SessionReservationService.UnitTests.TestUtils;

public static class CourtFactory
{
    public static Court CreateCourt(
        string name = "name",
        int maxDailySessions = 1,
        Guid? clubId = null,
        Guid? id = null)
    {
        return new Court(
            name: name,
            maxDailySessions: maxDailySessions,
            clubId: clubId ?? Guid.NewGuid(),
            id: id ?? Guid.NewGuid());
    }
}