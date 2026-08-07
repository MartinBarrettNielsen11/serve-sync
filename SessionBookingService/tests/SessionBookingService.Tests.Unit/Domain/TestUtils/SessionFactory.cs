using SessionBookingService.Domain.Common;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal static class SessionFactory
{
	internal static Session CreateSession(
		string name = SessionConstants.Name,
		string description = SessionConstants.Description,
		DateOnly? date = null,
		TimeSlot? timeSlot = null,
		int maxPlayerCapacity = SessionConstants.MaxPlayerCapacity,
		Guid? instructorId = null,
		Guid? courtId = null,
		List<SessionCategory>? categories = null,
		Guid? id = null)
	{
		return new Session(name,
							description,
							instructorId: instructorId ?? Guid.CreateVersion7(),
							courtId: courtId ?? Guid.CreateVersion7(),
							date: date ?? SessionConstants.Date,
							time: timeSlot ?? SessionConstants.Time,
							maxPlayerCapacity: maxPlayerCapacity,
							categories: categories ?? SessionConstants.Categories,
							id: id ?? Guid.NewGuid());
	}
}
