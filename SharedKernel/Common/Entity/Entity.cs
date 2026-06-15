namespace SharedKernel.Entity;

public abstract class Entity
{
    public Guid Id { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType()) return false;

        var representsSameEntity = Id == ((Entity)obj).Id;
        
        return representsSameEntity;
    }

    public override int GetHashCode() => Id.GetHashCode();
    protected Entity(Guid id) => Id = id;
    protected Entity() { }
}
