using ClubAdministrationService.Application.Courts.Commands.CreateCourt;
using ClubAdministrationService.Contracts.Courts;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Courts;

public sealed class Create : IEndpoint
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
                        onSuccess: r => TypedResults.CreatedAtRoute(routeName: $"rooms/{r.Id}", value: ""),
                        onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));

                    return result;
                })
            .WithTags(Tags.Courts)
            .WithSummary("Create court")
            .WithDescription("Create court for a club");
        //.RequireAuthorization();
    }
}
