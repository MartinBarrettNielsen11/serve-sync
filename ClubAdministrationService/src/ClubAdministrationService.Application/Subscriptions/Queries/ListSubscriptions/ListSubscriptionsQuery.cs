using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Queries.ListSubscriptions;

// add admin id, for now, return all
#pragma warning disable S2094
public record ListSubscriptionsQuery() : IRequest<Result<List<Subscription>>>;
#pragma warning restore S2094
