using ClubAdministrationService.Domain.ClubAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed class Subscription : RootAggregate
{
    private readonly List<Guid> _clubIds = new();
    private readonly int _maxCourtsAllowed;

    internal SubscriptionType SubscriptionType { get; } = null!;


    internal Subscription(SubscriptionType subscriptionType,
                          Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        SubscriptionType = subscriptionType;
        _maxCourtsAllowed = GetMaxCourtsAllowed();
    }

    internal int GetMaxClubs() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    internal int GetMaxCourtsAllowed() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };
    
    public int GetMaxDailySessionsAllowed() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };
    
    public Result AddClub(Club club)
    {
        if (_clubIds.Contains(club.Id))
            return Result.Failure(Error.Failure(code: "", description: "Gym already exists"));

        if (_maxCourtsAllowed < _clubIds.Count)
            return Result.Failure(SubscriptionErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
        
        _clubIds.Add(club.Id);
        
        return Result.Success<bool>(true);
    }
}
