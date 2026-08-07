namespace SharedKernel;

public abstract class RootAggregate : Entity
{
#pragma warning disable CA1051
	protected readonly ICollection<IDomainEvent> DomainEvents = [];
#pragma warning restore CA1051
	protected RootAggregate(Guid id) : base(id)
	{
	}

	protected RootAggregate()
	{
	}

	public ICollection<IDomainEvent> PopDomainEvents()
	{
		List<IDomainEvent> copy = DomainEvents.ToList();
		DomainEvents.Clear();

		return copy;
	}
}
