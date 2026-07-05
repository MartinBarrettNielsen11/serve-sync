namespace SharedKernel.Common;

#pragma warning disable MA0049
public abstract class Entity
#pragma warning restore MA0049
{
	protected Entity(Guid id)
	{
		Id = id;
	}

	protected Entity()
	{
	}

	public Guid Id { get; init; }

	public override bool Equals(object? obj)
	{
		if (obj is null || obj.GetType() != GetType()) return false;

		var representsSameEntity = Id == ((Entity)obj).Id;

		return representsSameEntity;
	}

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}
}