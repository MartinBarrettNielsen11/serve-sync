using SharedKernel.Results;

namespace Domain1.InstructorAggregate;

public static class InstructorErrors
{
    public static readonly Error CannotHaveMultipleOverlappingSessions = Error.Failure(
        "Instructor.CannotHaveMultipleOverlappingSessions",
        "An instructor cannot have two or more overlapping sessions");
}