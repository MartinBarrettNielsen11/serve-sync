using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Domain.AdminAggregate.Events;

internal sealed record SubscriptionSetEvent(Admin admin,  Subscription subscription);