using SharedKernel.Results;

namespace ServeSync.Domain.InstructorAggregate;

public static class InstructorErrors
{
    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Failure(
        "Instructor.CannotHaveTwoOrMoreOverlappingSessions",
        "An instructor cannot have two or more overlapping sessions");
}