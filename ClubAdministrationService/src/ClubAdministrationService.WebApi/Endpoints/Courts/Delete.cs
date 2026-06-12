using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Application.Courts.Commands.CreateCourt;
using ClubAdministrationService.Application.Courts.Commands.DeleteCourt;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Contracts.Courts;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.WebApi.Infrastructure;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Courts;

public sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(pattern: "courts/{clubId:guid}",
                handler: async (Guid clubId,
                    Guid courtId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    DeleteCourtCommand command = new(clubId, courtId);
                    
                    Result deleteCourtResult = await sender.Send(command, cancellationToken);

                    // TODO: Handle this result conflict thingy
                    IResult result = deleteCourtResult.Match(
                        onSuccess: _ => Results.NoContent(),
                        onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));
                    
                    return result;
                })
            .WithTags(Tags.Courts)
            .WithSummary("Delete court")
            .WithDescription("Delete court for a club");
        //.RequireAuthorization();
    }
}
