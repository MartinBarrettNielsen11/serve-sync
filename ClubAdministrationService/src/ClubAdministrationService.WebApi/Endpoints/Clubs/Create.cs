using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

/// <summary>
/// 
/// </summary>
public sealed class Create : IEndpoint
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
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
                    onSuccess: c => TypedResults.CreatedAtRoute(routeName: $"/subscriptions/{subscriptionId}/clubs/{c.Id}",
                                                                value: new ClubResponse(c.Id, c.Name)), 
                    onFailure: e => ProblemDetailsMapper.Problem([e.Error]));

                return result;
            })
            .WithTags(Tags.Clubs);
            //.RequireAuthorization();
    }
}
