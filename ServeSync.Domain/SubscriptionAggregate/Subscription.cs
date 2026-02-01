using SharedKernel;

namespace ServeSync.Domain.SubscriptionAggregate;

public sealed class Subscription : RootAggregate
{
        private readonly Guid _adminId;
        private readonly List<Guid> _clubIds = new();
        private readonly int _maxCourtsAllowed;

        public Subscription(Guid adminId,
                            Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            _maxCourtsAllowed = GetMaxGyms();
            _adminId = adminId;
        }

        public static int GetMaxGyms() => 1;

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
