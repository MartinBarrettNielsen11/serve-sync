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
													   request.TrainerId,
													   categoriesToDomainResult.Value);

					Result createSessionResult = await sender.Send(command, cancellationToken);

					IResult response = createSessionResult.Match(
						onSuccess: session => CreatedAtAction(
							nameof(GetSession),
							new { roomId, SessionId = session.Id },
							new SessionResponse(
								session.Id,
								session.Name,
								session.Description,
								session.NumParticipants,
								session.MaxParticipants,
								session.Date.ToDateTime(session.Time.Start),
								session.Date.ToDateTime(session.Time.End),
								session.Categories.Select(category => category.Name).ToList())),
						onFailure: err => ProblemDetailsMapper.Problem([err.Error]));

					return response;
				})
			.WithTags(Tags.Sessions)
			.WithSummary("Create session")
			.WithDescription("Create session for a court");
		//.RequireAuthorization();
	}
}

/*

[HttpPost]
public async Task<IActionResult> CreateSession(
	CreateSessionRequest request,
	Guid roomId)
{
	var command = new CreateSessionCommand(		roomId,
		request.Name,
		request.Description,
		request.MaxParticipants,
		request.StartDateTime,
		request.EndDateTime,
		request.TrainerId,
		categoriesToDomainResult.Value);

	var createSessionResult = await _sender.Send(command);

	return createSessionResult.Match(
		session => CreatedAtAction(
			nameof(GetSession),
			new { roomId, SessionId = session.Id },
			new SessionResponse(
				session.Id,
				session.Name,
				session.Description,
				session.NumParticipants,
				session.MaxParticipants,
				session.Date.ToDateTime(session.Time.Start),
				session.Date.ToDateTime(session.Time.End),
				session.Categories.Select(category => category.Name).ToList())),
		Problem);
}
*/
