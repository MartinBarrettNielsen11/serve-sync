using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;
using SharedKernel;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal static class SessionFactory
{
    internal static Session CreateSession(
        string name = SessionConstants.Name,
        string description = SessionConstants.Description,
        DateOnly? date = null,
        TimeSlot? timeRange = null,
        int maxPlayerCapacity = SessionConstants.MaxPlayerCapacity,
        Guid? instructorId = null,
        Guid? courtId = null,
        List<SessionCategory>? categories = null,
        Guid? id = null)
    {
        return new Session(
            name: name,
            description: description,
            instructorId: instructorId ?? Guid.CreateVersion7(),
            courtId: courtId ?? Guid.CreateVersion7(),
            date: date ?? SessionConstants.Date,
            time: timeRange ?? SessionConstants.Time,
            maxPlayerCapacity: maxPlayerCapacity,
            categories: categories ?? SessionConstants.Categories,
            id: id ?? Guid.NewGuid());
    }
}