using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Tests.Unit.Domain.Constants;

namespace SessionBookingService.Tests.Unit.Domain.TestUtils;

internal static class InstructorFactory
{
	internal static Instructor Create(Guid? userId = null, Guid? id = null)
	{
		return new Instructor(
			userId ?? Guid.CreateVersion7(),
			id: id ?? InstructorConstants.Id);
	}
}
