using Mediator;
using SessionBookingService.Application.Players.Commands.CancelBooking;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Players;

public sealed class CancelBooking : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete("{playerId:guid}/sessions/{sessionId:guid}/booking",
				async (Guid playerId,
					Guid sessionId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					CancelBookingCommand command = new(playerId, sessionId);

					Result cancelBookingResult = await sender.Send(command, cancellationToken);

					IResult response = cancelBookingResult.Match(
						Results.NoContent,
						err => ProblemDetailsMapper.Problem([err.Error]));

					return response;
				})
            .WithTags(Tags.Players)
            .MapToApiVersion(1)
			.WithSummary("Cancel booking")
			.WithDescription("Cancel booking for a session for a player");
	}
}
