using Mediator;
using Microsoft.AspNetCore.Mvc;
using SessionBookingService.Application.Bookings.Commands.CreateBooking;
using SessionBookingService.Application.Bookings.Commands.CreateSession;
using SessionBookingService.Contracts.Bookings;
using SessionBookingService.Contracts.Sessions;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.WebApi.Infrastructure;
using SessionBookingService.WebApi.Utils;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Sessions;

public sealed class CreateSession : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("courts/{courtId:guid}/sessions",
				async (CreateSessionRequest request,
					Guid courtId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					Result<List<SessionCategory>> categoriesToDomainResult = SessionCategoryUtils.ToDomain(request.Categories);

					if (categoriesToDomainResult.IsFailure)
					{
						return ProblemDetailsMapper.Problem([categoriesToDomainResult.Error]);
					}

					CreateSessionCommand command = new(courtId,
													   request.Name,
													   request.Description,
													   request.MaxPlayerCapacity,
													   request.StartDateTime,
													   request.EndDateTime,
													   request.InstructorId,
													   categoriesToDomainResult.Value);

					Result<Session> createSessionResult = await sender.Send(command, cancellationToken);

					IResult response = createSessionResult.Match(
						onSuccess: s => TypedResults.CreatedAtRoute(
										routeName: nameof(GetSession),
										routeValues: new {courtId, sessionId = createSessionResult.Value.Id},
										value: new SessionResponse(
												s.Id,
												s.Name,
												s.Description,
												s.NumPlayers,
												s.MaxPlayerCapacity,
												s.Date.ToDateTime(s.Time.Start),
												s.Date.ToDateTime(s.Time.End),
												s.Categories.Select(c => c.Name).ToList())),
						onFailure: err => ProblemDetailsMapper.Problem([err.Error]));

					return response;
				})
			.WithTags(Tags.Sessions)
			.WithSummary("Create session")
			.WithDescription("Create session for a court")
			.Produces<SessionResponse>(StatusCodes.Status201Created);
		//.RequireAuthorization();
	}
}
