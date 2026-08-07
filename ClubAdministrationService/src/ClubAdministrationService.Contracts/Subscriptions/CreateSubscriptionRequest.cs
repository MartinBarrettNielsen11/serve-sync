namespace ClubAdministrationService.Contracts.Subscriptions;

public sealed record CreateSubscriptionRequest(SubscriptionType SubscriptionType, Guid AdminId);
