namespace ServeSync.Domain.SubscriptionAggregate;

public static class SubscriptionErrors
{
    public static readonly Error NumberOfCourtsCannotExceedSubscriptionLimit = Error.Failure(
        "Subscription.NumberOfCourtsCannotExceedSubscriptionLimit",
        "A subscription cannot have more courts than the subscription allows");
}