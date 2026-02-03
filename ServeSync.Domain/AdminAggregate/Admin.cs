using SharedKernel;

namespace ServeSync.Domain.AdminAggregate;

public class Admin : RootAggregate
{
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    public Admin(
        Guid userId,
        Guid subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}
