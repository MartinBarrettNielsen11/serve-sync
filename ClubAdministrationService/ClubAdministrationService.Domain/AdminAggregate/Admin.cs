using SharedKernel;

namespace Domain3.AdminAggregate;

public class Admin : RootAggregate
{
    private readonly Guid _userId;
    private readonly Guid _subscriptionId;

    public Admin(
        Guid userId,
        Guid subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        _userId = userId;
        _subscriptionId = subscriptionId;
    }
}
