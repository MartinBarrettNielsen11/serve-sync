using SharedKernel.Results;

namespace ServeSync.Domain.PlayerAggregate;

public static class PlayerErrors
{
    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions =
        Error.Failure(
            "Player.CannotHaveTwoOrMoreOverlappingSessions",
            "A player cannot have two or more overlapping sessions");
}