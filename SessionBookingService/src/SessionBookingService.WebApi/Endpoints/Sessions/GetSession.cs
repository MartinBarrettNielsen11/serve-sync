using Mediator;
using SessionBookingService.Application.Sessions.Queries.GetSession;
using SessionBookingService.Contracts.Sessions;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Sessions;

public sealed class GetSession : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courts/{courtId:guid}/sessions/{sessionId:guid}",
					async (Guid courtId, Guid sessionId, ISender sender, CancellationToken cancellationToken) =>
					{
						GetSessionQuery query = new(courtId, sessionId);

						Result<Session> getSessionResult = await sender.Send(query, cancellationToken);

						IResult response = getSessionResult.Match(s => Results.Ok(new SessionResponse(s.Id,
																									s.Name,
																									s.Description,
																									s.NumPlayers,
																									s.MaxPlayerCapacity,
																									s.Date
																									.ToDateTime(s.Time
																												.Start),
																									s.Date
																									.ToDateTime(s.Time
																												.End),
																									s.Categories
																									.Select(category =>
																												category
																													.Name)
																									.ToList())),
																e => ProblemDetailsMapper.Problem([e.Error]));

						return response;
					})
			.WithTags(Tags.Sessions)
			.WithName("GetSession")
			.WithSummary("Get session for court")
			.WithDescription("Get session for courtId")
			.Produces<SessionResponse>();
	}
}
