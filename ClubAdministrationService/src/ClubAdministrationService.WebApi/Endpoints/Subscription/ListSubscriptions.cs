using ClubAdministrationService.Application.Subscriptions.Queries.ListSubscriptions;
using ClubAdministrationService.Contracts.Subscriptions;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Subscription;

public sealed class ListSubscriptions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "subscriptions",
                   handler: async (ISender sender, CancellationToken cancellationToken) =>
                {
                    // get user/admin id from token, for now, return all
                    ListSubscriptionsQuery query = new();
                    
                    Result<List<Domain.SubscriptionAggregate.Subscription>> listSubscriptionResult = await sender.Send(query, cancellationToken);

                    IResult result = listSubscriptionResult.Match(
                        onSuccess: ss => Results.Ok(ss.ConvertAll(s => new SubscriptionResponse(Id: s.Id, SubscriptionType: ToDto(s.SubscriptionType)))),
                        onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

                    return result;
                })
            .WithTags(Tags.Subscription)
            .WithSummary("List subscriptions")
            .WithDescription("List subscriptions");
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
