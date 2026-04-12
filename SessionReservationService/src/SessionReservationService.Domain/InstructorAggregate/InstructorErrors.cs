using SharedKernel.Results;

namespace SessionReservationService.Domain.InstructorAggregate;

public static class InstructorErrors
{
    public static readonly Error SessionCannotOverlap = Error.Failure(
        "Instructor.CannotHaveMultipleOverlappingSessions",
        "An instructor cannot have two or more overlapping sessions");
}