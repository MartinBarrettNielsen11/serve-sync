using SharedKernel.Common;

namespace SharedKernel;

public abstract class RootAggregate : Entity
{
    protected RootAggregate(Guid id) : base(id) { }

    protected RootAggregate() { }
    
    protected readonly ICollection<IDomainEvent> DomainEvents = [];

    public ICollection<IDomainEvent> PopDomainEvents()
    {
        var copy = DomainEvents.ToList();
        DomainEvents.Clear();

        return copy;
    }
}