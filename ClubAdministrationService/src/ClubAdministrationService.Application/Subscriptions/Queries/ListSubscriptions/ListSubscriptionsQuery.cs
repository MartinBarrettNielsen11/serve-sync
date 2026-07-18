using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Queries.ListSubscriptions;

// add admin id, for now, return all
public record ListSubscriptionsQuery : IRequest<Result<List<Subscription>>>;
