using System;
using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.UnitTests.Domain.TestUtils;

internal static class CourtFactory
{
    internal static Court Create(
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