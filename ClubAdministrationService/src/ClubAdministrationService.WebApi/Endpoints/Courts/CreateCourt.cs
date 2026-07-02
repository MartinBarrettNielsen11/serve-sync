using ClubAdministrationService.Application.Courts.Commands.CreateCourt;
using ClubAdministrationService.Contracts.Courts;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Courts;

public sealed class CreateCourt : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "clubs/{clubId:guid}/courts",
                handler: async (CreateCourtRequest request,
                    Guid clubId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    CreateCourtCommand command = new(clubId, request.Name);
                    
                    Result<Court> createCourtResult = await sender.Send(command, cancellationToken);

                    IResult result = createCourtResult.Match(
                        onSuccess: r => TypedResults.CreatedAtRoute(routeName: "GetCourt", 
                                                                    routeValues: new { clubId, courtId = r.Id },
                                                                    value: new CourtResponse(r.Id, r.Name)),
                        onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));

                    return result;
                })
            .WithTags(Tags.Courts)
            .WithSummary("Create court")
            .WithDescription("Create court for a club")
            .Produces<CourtResponse>(StatusCodes.Status201Created);
        //.RequireAuthorization();
    }
}
