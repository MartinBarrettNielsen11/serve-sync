using Mediator;
using Microsoft.AspNetCore.Mvc;
using SessionBookingService.Application.Clubs.Queries.ListSessions;
using SessionBookingService.Contracts.Sessions;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SessionBookingService.WebApi.Utils;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Clubs;

public sealed class ListSessions : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("{clubId:guid}/sessions",
				async (
					Guid clubId,
					ISender sender,
					CancellationToken cancellationToken,
					DateTime? startDateTime = null,
					DateTime? endDateTime = null,
					[FromQuery] string[]? categories = null) =>
				{
					Result<List<SessionCategory>> categoriesToDomainResult = SessionCategoryUtils.ToDomain(categories);

					if (categoriesToDomainResult.IsFailure)
					{
						IResult problem = ProblemDetailsMapper.Problem([categoriesToDomainResult.Error]);
						return problem;
					}

					ListSessionsQuery query = new(clubId, startDateTime, endDateTime, categoriesToDomainResult.Value);

					Result<List<Session>> listSessionsResult = await sender.Send(query, cancellationToken);

					IResult res = listSessionsResult.Match(
						sessions => Results.Ok(sessions.ConvertAll(s => new SessionResponse(
							s.Id,
							s.Name,
							s.Description,
							s.NumPlayers,
							s.MaxPlayerCapacity,
							s.Date.ToDateTime(s.Time.Start),
							s.Date.ToDateTime(s.Time.End),
							s.Categories.Select(category => category.Name).ToList()))),
						errors => ProblemDetailsMapper.Problem([errors.Error]));

					return res;
				})
			.WithTags(Tags.Clubs)
			.WithSummary("List sessions for a club")
			.WithDescription("List sessions for a club")
			.Produces<SessionResponse>();
	}
}
