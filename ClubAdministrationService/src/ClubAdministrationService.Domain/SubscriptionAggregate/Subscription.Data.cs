namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed partial class Subscription
{
	private readonly List<Guid> _clubIds = [];
	private readonly int _maxCourtsAllowed;

	internal Subscription(SubscriptionType subscriptionType,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		SubscriptionType = subscriptionType;
		_maxCourtsAllowed = GetMaxCourtsAllowed();
	}

	private Subscription()
	{
	} // For EF / serialization

	internal IReadOnlyCollection<Guid> ClubIds => _clubIds.AsReadOnly();

	internal SubscriptionType SubscriptionType { get; } = null!;
}