using ClubAdministrationService.Domain.ClubAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.SubscriptionAggregate.Events;

internal sealed record ClubAddedToSubscriptionEvent(Subscription Subscription, Club Club) : IDomainEvent;
