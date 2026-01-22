namespace SharedKernel;

public abstract class Entity(Guid id)
{
    protected Guid Id { get; } = id;

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var representsSameEntity = Id == ((Entity)obj).Id;
        
        return representsSameEntity;
    }

    public override int GetHashCode() => Id.GetHashCode();
}
