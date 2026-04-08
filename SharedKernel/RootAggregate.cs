using SharedKernel.Common;

namespace SharedKernel;

public abstract class RootAggregate : Entity
{
    protected RootAggregate(Guid id) : base(id) { }

    protected RootAggregate()
    {
    }
}