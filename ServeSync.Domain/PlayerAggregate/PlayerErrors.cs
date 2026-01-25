using SharedKernel.Results;

namespace ServeSync.Domain.PlayerAggregate;

public static class PlayerErrors
{
    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions =
        Error.Conflict(
            "Participant.CannotHaveTwoOrMoreOverlappingSessions",
            "A participant cannot have two or more overlapping sessions");
}