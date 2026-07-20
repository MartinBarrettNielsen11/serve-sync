using Mediator;
using SessionBookingService.Application.Players.Queries.ListPlayerSessions;
using SessionBookingService.Contracts.Sessions;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Players;

public sealed class ListPlayers : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("{playerId:guid}/sessions",
				async (
					Guid playerId,
					ISender sender,
					CancellationToken cancellationToken,
					DateTime? startDateTime = null,
					DateTime? endDateTime = null) =>
				{
					ListPlayersSessionsQuery query = new(playerId, endDateTime, startDateTime);

					Result<List<Session>> listPlayerSessionsResult = await sender.Send(query, cancellationToken);

					IResult result = listPlayerSessionsResult.Match(
						onSuccess: sessions => Results.Ok(sessions.ConvertAll(s => new SessionResponse(
							s.Id,
							s.Name,
							s.Description,
							s.NumPlayers,
							s.MaxPlayerCapacity,
							s.Date.ToDateTime(s.Time.Start),
							s.Date.ToDateTime(s.Time.End),
							s.Categories.Select(category => category.Name).ToList()))),
						onFailure: errors => ProblemDetailsMapper.Problem([errors.Error]));

					return result;
				})
			.WithTags(Tags.Players)
			.WithSummary("List players for a session")
			.WithDescription("List players for a session")
			.Produces<SessionResponse>();
	}
}
