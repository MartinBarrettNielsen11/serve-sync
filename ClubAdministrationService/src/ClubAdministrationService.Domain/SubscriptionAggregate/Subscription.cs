using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed class Subscription : RootAggregate
{
    private readonly Guid _adminId;
    private readonly List<Guid> _clubIds = new();
    private readonly int _maxCourtsAllowed;

    public SubscriptionType SubscriptionType { get; } = null!;


    internal Subscription(SubscriptionType subscriptionType,
                          Guid adminId,
                          Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        SubscriptionType = subscriptionType;
        _maxCourtsAllowed = GetMaxCourtsAllowed();
        _adminId = adminId;
    }

    internal int GetMaxCourtsAllowed() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public Result AddClub(Guid clubId)
    {
        if (_clubIds.Contains(clubId))
            return Result.Failure(Error.Failure(code: "", description: "Gym already exists"));

        if (_maxCourtsAllowed < _clubIds.Count)
            return Result.Failure(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
        
        _clubIds.Add(clubId);
        
        return Result.Success();
    }
}
