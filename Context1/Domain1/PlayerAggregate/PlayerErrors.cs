using SharedKernel.Results;

namespace Domain1.PlayerAggregate;

public static class PlayerErrors
{
    public static readonly Error CannotHaveTwoOrMoreOverlappingSessions =
        Error.Failure(
            "Player.CannotHaveTwoOrMoreOverlappingSessions",
            "A player cannot have two or more overlapping sessions");
}