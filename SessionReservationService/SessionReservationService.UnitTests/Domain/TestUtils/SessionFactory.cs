using SessionReservationService.Domain.SessionAggregate;
using SessionReservationService.UnitTests.Domain.Constants;
using SharedKernel;

namespace SessionReservationService.UnitTests.Domain.TestUtils;

internal static class SessionFactory
{
    internal static Session CreateSession(
        string name = SessionConstants.Name,
        string description = SessionConstants.Description,
        DateOnly? date = null,
        TimeSlot? timeRange = null,
        int maxPlayerCapacity = SessionConstants.MaxPlayerCapacity,
        Guid? instructorId = null,
        Guid? id = null)
    {
        return new Session(
            name: name,
            description: description,
            instructorId: instructorId ?? Guid.CreateVersion7(),
            date: date ?? SessionConstants.Date,
            time: timeRange ?? SessionConstants.Time,
            maxPlayerCapacity: maxPlayerCapacity,
            id: id ?? Guid.NewGuid());
    }
}