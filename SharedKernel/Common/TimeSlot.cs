using SharedKernel.ValueObjects;

namespace SharedKernel;

public class TimeSlot : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeSlot(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }
    
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
    
    public bool IsOverlappingWith(TimeSlot other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;
    }
}
