using ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;
using ClubAdministrationService.Contracts.Subscriptions;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Subscription;

public sealed class CreateCourt : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("subscriptions",
					async (
						CreateSubscriptionRequest request,
						ISender sender,
						CancellationToken cancellationToken) =>
					{
						if (!Domain.SubscriptionAggregate.SubscriptionType
									.TryFromName(request.SubscriptionType.ToString(),
												out Domain.SubscriptionAggregate.SubscriptionType? subscriptionType))
						{
							return Results.Problem("Invalid subscription type",
													statusCode: StatusCodes.Status400BadRequest);
						}

						CreateSubscriptionCommand command = new(subscriptionType, request.AdminId);

						Result<Domain.SubscriptionAggregate.Subscription> createSubscriptionResult =
							await sender.Send(command, cancellationToken);

						IResult response =
							createSubscriptionResult.Match(s => Results.Ok(new SubscriptionResponse(s.Id,
																									ToDto(
																										s.SubscriptionType))),
															errors => ProblemDetailsMapper.Problem([errors.Error]));

						return response;
					})
			.WithTags(Tags.Subscription)
			.WithSummary("Create subscription")
			.WithDescription("Create subscription");
		//.RequireAuthorization();
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
