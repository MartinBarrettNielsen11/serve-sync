
namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed partial class Subscription
{
    private readonly List<Guid> _clubIds = new();
    private readonly int _maxCourtsAllowed;

    private SubscriptionType SubscriptionType { get; } = null!;

    internal Subscription(SubscriptionType subscriptionType,
                          Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        SubscriptionType = subscriptionType;
        _maxCourtsAllowed = GetMaxClubsAllowed();
    }

    private Subscription() { } // For EF / serialization
}