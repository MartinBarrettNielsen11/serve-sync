using System;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.UnitTests.Domain.Constants;

namespace SessionBookingService.UnitTests.Domain.TestUtils;

internal static class InstructorFactory
{
    internal static Instructor Create(Guid? userId = null, Guid? id = null)
    {
        return new Instructor(
            userId: userId ?? Guid.CreateVersion7(),
            id: id ?? InstructorConstants.Id);
    }
}