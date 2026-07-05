using ClubAdministrationService.Application.Subscriptions.Queries.ListSubscriptions;
using ClubAdministrationService.Contracts.Subscriptions;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Subscription;

public sealed class ListSubscriptions : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("subscriptions",
				async (ISender sender, CancellationToken cancellationToken) =>
				{
					// get user/admin id from token, for now, return all
					ListSubscriptionsQuery
						query = new(); // Try to reuse the object instance using caching: https://www.youtube.com/watch?v=aaFLtcf8cO4

					Result<List<Domain.SubscriptionAggregate.Subscription>> listSubscriptionResult =
						await sender.Send(query, cancellationToken);

					IResult result = listSubscriptionResult.Match(
						ss => Results.Ok(ss.ConvertAll(s => new SubscriptionResponse(s.Id, ToDto(s.SubscriptionType)))),
						errors => ProblemDetailsMapper.Problem([errors.Error]));

					return result;
				})
			.WithTags(Tags.Subscription)
			.WithSummary("List subscriptions")
			.WithDescription("List subscriptions");
	}

	private static SubscriptionType ToDto(Domain.SubscriptionAggregate.SubscriptionType subscriptionType)
	{
		return subscriptionType.Name switch
		{
			nameof(SubscriptionType.Free) => SubscriptionType.Free,
			nameof(SubscriptionType.Starter) => SubscriptionType.Starter,
			nameof(SubscriptionType.Pro) => SubscriptionType.Pro,
			_ => throw new InvalidOperationException()
		};
	}
}