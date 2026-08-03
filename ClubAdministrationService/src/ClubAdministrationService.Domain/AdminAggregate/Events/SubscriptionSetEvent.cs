using ClubAdministrationService.Domain.SubscriptionAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.AdminAggregate.Events;

internal sealed record SubscriptionSetEvent(Admin Admin, Subscription Subscription) : IDomainEvent;
