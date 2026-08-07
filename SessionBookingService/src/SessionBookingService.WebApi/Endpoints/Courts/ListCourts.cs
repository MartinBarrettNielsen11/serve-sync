using Mediator;
using SessionBookingService.Application.Courts.Queries.ListCourts;
using SessionBookingService.Contracts.Courts;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Courts;

public sealed class ListCourts : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("clubs/{clubId:Guid}/courts",
				async (Guid clubId, ISender sender, CancellationToken cancellationToken) =>
				{
					ListCourtsQuery query = new(clubId);

					Result<List<Court>> listCourtsResult = await sender.Send(query, cancellationToken);

					IResult response = listCourtsResult.Match(
						c => Results.Ok(c.ConvertAll(cc => new CourtResponse(cc.Id, cc.Name))),
						f => ProblemDetailsMapper.Problem([f.Error]));

					return response;
				})
			.WithTags(Tags.Courts)
			.WithName("List Courts")
			.WithSummary("Lists court for a given club")
			.WithDescription("List court for a specified club id")
			.Produces<CourtResponse>();
	}
}
