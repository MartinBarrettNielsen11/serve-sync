using ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;
using ClubAdministrationService.Contracts.Subscriptions;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Subscription;

public sealed class CreateClub : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "subscriptions",
                handler: async (
                    CreateSubscriptionRequest request,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    if (!Domain.SubscriptionAggregate.SubscriptionType.TryFromName(request.SubscriptionType.ToString(), out Domain.SubscriptionAggregate.SubscriptionType? subscriptionType))
                    {
                        return Results.Problem(detail: "Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
                    }
                    
                    CreateSubscriptionCommand command = new(subscriptionType, request.AdminId);

                    Result<Domain.SubscriptionAggregate.Subscription> createSubscriptionResult = await sender.Send(command, cancellationToken);
        
                    IResult response = createSubscriptionResult.Match(
                        onSuccess: s => Results.Ok(new SubscriptionResponse(s.Id, ToDto(s.SubscriptionType))),
                        onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

                    return response;

                })
            .WithTags(Tags.Subscription)
            .WithSummary("Create subscription")
            .WithDescription("Create subscription");
        //.RequireAuthorization();
    }
    
    private static Contracts.Subscriptions.SubscriptionType ToDto(Domain.SubscriptionAggregate.SubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(SubscriptionType.Free) => Contracts.Subscriptions.SubscriptionType.Free,
            nameof(SubscriptionType.Starter) => Contracts.Subscriptions.SubscriptionType.Starter,
            nameof(SubscriptionType.Pro) => Contracts.Subscriptions.SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }
}
