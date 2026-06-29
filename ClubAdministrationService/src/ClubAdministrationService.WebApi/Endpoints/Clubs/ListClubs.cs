using ClubAdministrationService.Application.Clubs.ListClubs;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class ListClubs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "subscriptions/{subscriptionId:guid}/clubs",
                handler: async (
                    Guid subscriptionId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    ListClubsQuery command = new(subscriptionId);

                    Result<List<Club>> listClubsResult = await sender.Send(command, cancellationToken);

                    IResult l = listClubsResult.Match(
                        onSuccess: clubs => Results.Ok(clubs.ConvertAll(c => new ClubResponse(c.Id, c.Name))),
                        onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

                    return l;
                })
            .WithTags(Tags.Clubs)
            .WithSummary("List clubs")
            .WithDescription("List clubs for a subscription");
    }
}