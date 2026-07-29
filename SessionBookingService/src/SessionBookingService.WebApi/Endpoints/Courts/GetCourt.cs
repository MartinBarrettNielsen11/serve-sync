using Mediator;
using SessionBookingService.Application.Courts.Queries.GetCourt;
using SessionBookingService.Contracts.Courts;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Courts;

public sealed class GetCourt : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("clubs/{clubId:Guid}/courts/{courtId:Guid}",
				async (Guid clubId,
					Guid courtId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					GetCourtQuery query = new(clubId, courtId);

					Result<Court> getRoomResult = await sender.Send(query, cancellationToken);

					IResult response = getRoomResult.Match(
						onSuccess: c => Results.Ok(new CourtResponse(c.Id, c.Name)),
						onFailure: e => ProblemDetailsMapper.Problem([e.Error]));

					return response;
				})
			.WithTags(Tags.Courts)
            .MapToApiVersion(1)
			.WithName("GetCourt")
			.WithSummary("Get court")
			.WithDescription("Get court for specific clubId")
			.Produces<CourtResponse>();
	}
}
