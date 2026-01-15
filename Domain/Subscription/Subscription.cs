namespace domain.Subscription;

public class Subscription
{
        private readonly Guid _id;
        private readonly List<Guid> _gymIds = new();
        private readonly int _maxGyms;
        private readonly Guid _adminId;
}