using SessionReservationService.Domain.SessionAggregate;
using SharedKernel;

namespace SessionReservationService.UnitTests.TestUtils;

public class SessionFactory
{
    public static Session CreateSession(
        string name,
        Guid instructorId,
        DateOnly date,
        TimeRange timeRange,
        int maxPlayerCapacity,
        Guid? id = null)
    {
        return new Session(
            name: name,
            instructorId: instructorId,
            date: date,
            time: timeRange,
            maxPlayerCapacity: maxPlayerCapacity,
            id: id ?? Guid.NewGuid());
    }
}