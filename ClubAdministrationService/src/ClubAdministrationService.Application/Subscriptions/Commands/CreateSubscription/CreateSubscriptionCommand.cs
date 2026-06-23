using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

internal sealed record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<Result<Subscription>>;