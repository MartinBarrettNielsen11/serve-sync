using ClubAdministrationService.Domain.SubscriptionAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.AdminAggregate;

internal sealed class Admin : RootAggregate
{
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    public Admin(
        Guid userId,
        Guid? subscriptionId,
        Guid? id = null)
        : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }
    
    public void SetSubscription(Subscription subscription)
    {
        if (SubscriptionId is not null)
        {
            throw new InvalidOperationException();
        }
        
        SubscriptionId = subscription.Id;

        // add domian event
    }
    
    private Admin() { }

}
