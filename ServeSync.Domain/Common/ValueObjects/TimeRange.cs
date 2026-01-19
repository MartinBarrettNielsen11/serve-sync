namespace ServeSync.Domain.Common;

public class TimeRange(TimeOnly start, TimeOnly end)
{
    public TimeOnly Start { get; init; } = start;
    public TimeOnly End { get; init; } = end;
}