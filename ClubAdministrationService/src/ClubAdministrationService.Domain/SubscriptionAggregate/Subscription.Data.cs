
namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed partial class Subscription
{
    private readonly List<Guid> _clubIds = [];
    internal IReadOnlyCollection<Guid> ClubIds => _clubIds.AsReadOnly();
    private readonly int _maxCourtsAllowed;

    internal SubscriptionType SubscriptionType { get; private set; } = null!;

    internal Subscription(SubscriptionType subscriptionType,
                          Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        SubscriptionType = subscriptionType;
        _maxCourtsAllowed = GetMaxCourtsAllowed();
    }

    private Subscription() { } // For EF / serialization
}