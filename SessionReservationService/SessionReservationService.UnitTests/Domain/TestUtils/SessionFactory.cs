using SessionReservationService.Domain.SessionAggregate;
using SessionReservationService.UnitTests.Domain.Constants;
using SharedKernel;

namespace SessionReservationService.UnitTests.Domain.TestUtils;

internal static class SessionFactory
{
    internal static Session CreateSession(
        DateOnly date,
        TimeRange timeRange,
        int maxPlayerCapacity,
        string name = SessionConstants.Name,
        string description = SessionConstants.Description,
        Guid? instructorId = null,
        Guid? id = null)
    {
        return new Session(
            name: name,
            description: description,
            instructorId: instructorId ?? Guid.CreateVersion7(),
            date: date,
            time: timeRange,
            maxPlayerCapacity: maxPlayerCapacity,
            id: id ?? Guid.NewGuid());
    }
}