using SharedKernel.Results;

namespace SessionBookingService.Domain.InstructorAggregate;

public static class InstructorErrors
{
    public static readonly Error SessionCannotOverlap = Error.Failure(
        "Instructor.CannotHaveMultipleOverlappingSessions",
        "An instructor cannot have two or more overlapping sessions");
}