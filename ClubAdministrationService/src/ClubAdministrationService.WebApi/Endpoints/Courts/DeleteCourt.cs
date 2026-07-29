using ClubAdministrationService.Application.Courts.Commands.DeleteCourt;
using ClubAdministrationService.WebApi.Infrastructure;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Endpoints.Courts;

public sealed class DeleteCourt : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete("clubs/{clubId:guid}courts/{courtId:guid}",
				async (Guid clubId,
					Guid courtId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					DeleteCourtCommand command = new(clubId, courtId);

					Result deleteCourtResult = await sender.Send(command, cancellationToken);

					IResult result = deleteCourtResult.Match(
						Results.NoContent,
						err => ProblemDetailsMapper.Problem([err.Error]));

					return result;
				})
			.WithTags(Tags.Courts)
            .MapToApiVersion(1)
			.WithSummary("Delete court")
			.WithDescription("Delete court for a club");
		//.RequireAuthorization();
	}
}
