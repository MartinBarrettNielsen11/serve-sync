using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

internal sealed record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId) : IRequest<Result<Subscription>>;