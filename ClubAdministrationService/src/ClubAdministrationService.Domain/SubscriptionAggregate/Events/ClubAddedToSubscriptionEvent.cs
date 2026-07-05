using ClubAdministrationService.Domain.ClubAggregate;
using SharedKernel;

namespace ClubAdministrationService.Domain.SubscriptionAggregate.Events;

#pragma warning disable MSG0005
internal sealed record ClubAddedToSubscriptionEvent(Subscription Subscription, Club Club) : IDomainEvent;
#pragma warning restore MSG0005