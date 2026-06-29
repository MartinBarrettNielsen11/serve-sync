using ClubAdministrationService.Application.Clubs.GetClub;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

public sealed class GetClub : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(pattern: "subscriptions/{subscriptionId:guid}/clubs/{clubId:guid}",
                handler: async (Guid subscriptionId,
                    Guid clubId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    GetClubQuery query = new(subscriptionId, clubId);

                    Result<Club> getClubResult = await sender.Send(query, cancellationToken);

                    IResult res = getClubResult.Match(
                        onSuccess: club => Results.Ok(new ClubResponse(club.Id, club.Name)),
                        onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

                    return res;
                })
            .WithTags(Tags.Clubs)
            .WithName("GetClub")
            .WithSummary("Get club")
            .WithDescription("Get club for specific clubId and subscriptionId");
    }
}