namespace ClubAdministrationService.Domain.ClubAggregate;

internal sealed partial class Club
{
	private readonly List<Guid> _courtIds = [];
	private readonly List<Guid> _instructorIds = [];

	private readonly int _maxCourtCapacity;

	internal Club(string name,
		int maxCourtCapacity,
		Guid subscriptionId,
		Guid? id = null) : base(id ?? Guid.CreateVersion7())
	{
		Name = name;
		SubscriptionId = subscriptionId;
		_maxCourtCapacity = maxCourtCapacity;
	}

	private Club()
	{
	} // For EF / serialization

	public string Name { get; } = null!;
	public Guid SubscriptionId { get; }

	public IReadOnlyList<Guid> CourtIds => _courtIds;
}