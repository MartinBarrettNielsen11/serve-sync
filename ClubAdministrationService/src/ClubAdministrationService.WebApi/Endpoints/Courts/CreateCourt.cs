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
		app.MapPost("clubs/{clubId:guid}/courts",
				async (CreateCourtRequest request,
					Guid clubId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					CreateCourtCommand command = new(clubId, request.Name);

					Result<Court> createCourtResult = await sender.Send(command, cancellationToken);

					IResult result = createCourtResult.Match(
						r => TypedResults.CreatedAtRoute(
							routeName: "GetCourt", // this is not right - you dont have that endpoint
							routeValues: new { clubId, courtId = r.Id },
							value: new CourtResponse(r.Id, r.Name)),
						err => ProblemDetailsMapper.Problem([err.Error]));

					return result;
				})
			.WithTags(Tags.Courts)
			.WithSummary("Create court")
			.WithDescription("Create court for a club")
			.Produces<CourtResponse>(StatusCodes.Status201Created);
		//.RequireAuthorization();
	}
}
