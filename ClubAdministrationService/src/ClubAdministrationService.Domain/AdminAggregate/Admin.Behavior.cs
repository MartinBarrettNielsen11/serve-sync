using SharedKernel;

namespace ClubAdministrationService.Domain.AdminAggregate;

internal sealed partial class Admin : RootAggregate
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    internal Admin(
        Guid userId,
        Guid? subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    private Admin() { } // For EF / serialization
}