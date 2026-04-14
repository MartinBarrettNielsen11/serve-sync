using SessionBookingService.UnitTests.Domain.Constants;

namespace SessionBookingService.UnitTests.Domain.TestUtils;

internal static class InstructorFactory
{
    public static Instructor Create(Guid? userId = null, Guid? id = null)
    {
        return new Instructor(
            userId: userId ?? Guid.NewGuid(),
            id: id ?? InstructorConstants.Id);
    }
}