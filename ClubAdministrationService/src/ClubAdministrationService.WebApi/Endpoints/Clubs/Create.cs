using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;
#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "subscriptions/{subscriptionId:guid}/clubs",
                handler: async (CreateClubRequest request,
                    Guid subscriptionId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    CreateClubCommand command = new(request.Name, subscriptionId);

                    Result<Club> createClubResult = await sender.Send(command, cancellationToken);

                    IResult result = createClubResult.Match(
                        onSuccess: c => TypedResults.CreatedAtRoute(
                            routeName: $"/subscriptions/{subscriptionId}/clubs/{c.Id}",
                            value: new ClubResponse(c.Id, c.Name)),
                        onFailure: e => ProblemDetailsMapper.Problem([e.Error]));

                    return result;
                })
            .WithTags(Tags.Clubs)
            .WithSummary("Create club")
            .WithDescription("Create club for a subscription");
        //.RequireAuthorization();
    }
}
