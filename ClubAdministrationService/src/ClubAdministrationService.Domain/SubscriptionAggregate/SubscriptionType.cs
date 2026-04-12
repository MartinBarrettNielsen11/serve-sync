using Ardalis.SmartEnum;

namespace ClubAdministrationService.Domain.SubscriptionAggregate;

internal sealed class SubscriptionType : SmartEnum<SubscriptionType>
{
    internal static readonly SubscriptionType Free = new(nameof(Free), 0);
    internal static readonly SubscriptionType Starter = new(nameof(Starter), 1);
    internal static readonly SubscriptionType Pro = new(nameof(Pro), 2);

    public SubscriptionType(string name, int value) : base(name, value)
    {
    }
}