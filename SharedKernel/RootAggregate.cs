namespace SharedKernel;

public abstract class RootAggregate : Entity
{
	private readonly List<IDomainEvent> _domainEvents = [];
	protected RootAggregate(Guid id) : base(id)
	{
	}

	protected RootAggregate()
	{
	}

	protected void RaiseDomainEvent(IDomainEvent domainEvent)
	{
		ArgumentNullException.ThrowIfNull(domainEvent);

		_domainEvents.Add(domainEvent);
	}


	public ICollection<IDomainEvent> PopDomainEvents()
	{
		List<IDomainEvent> copy = _domainEvents.ToList();
		_domainEvents.Clear();

		return copy;
	}
}
