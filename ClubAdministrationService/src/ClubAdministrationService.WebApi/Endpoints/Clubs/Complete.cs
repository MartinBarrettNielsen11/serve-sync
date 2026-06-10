using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Clubs;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(pattern: "todos/{subscriptionId:guid}/complete", 
                   handler: async (CreateClubRequest request, 
                            Guid subscriptionId, 
                            ISender sender, 
                            CancellationToken cancellationToken) =>
            {
                
                CreateClubCommand command = new(request.Name, subscriptionId);

                Result<Club> createClubResult = await sender.Send(command, cancellationToken);

                // TODO: Replace CustomResults with something else. 
                IResult yo = createClubResult.Match(onSuccess: c => TypedResults.CreatedAtRoute(routeName: $"/subscriptions/{subscriptionId}/clubs/{c.Id}",
                                                                                                value: new ClubResponse(c.Id, c.Name)), 
                    onFailure: CustomResults.Problem);

                return yo;
            })
            .WithTags(Tags.Clubs);
            //.RequireAuthorization();
    }
}
