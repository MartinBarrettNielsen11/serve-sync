namespace SharedKernel;

public abstract class RootAggregate : Entity.Entity
{
    protected RootAggregate(Guid id) : base(id) { }

    protected RootAggregate() { }
    
#pragma warning disable CA1051
    protected readonly ICollection<IDomainEvent> DomainEvents = [];
#pragma warning restore CA1051

    public ICollection<IDomainEvent> PopDomainEvents()
    {
        var copy = DomainEvents.ToList();
        DomainEvents.Clear();

        return copy;
    }
}