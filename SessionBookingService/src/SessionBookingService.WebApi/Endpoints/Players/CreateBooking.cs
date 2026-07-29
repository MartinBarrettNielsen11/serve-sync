using Mediator;
using SessionBookingService.Application.Bookings.Commands.CreateBooking;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Players;

public sealed class CreateBooking : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("{playerId:guid}/sessions/{sessionId:guid}/booking",
			async (Guid playerId,
				   Guid sessionId,
				   ISender sender,
				   CancellationToken cancellationToken) =>
			{
				CreateBookingCommand command = new(sessionId, playerId);

				Result<Guid> createBookingResult = await sender.Send(command, cancellationToken);

				IResult response = createBookingResult.Match(
					onSuccess: Results.NoContent,
					onFailure: err => ProblemDetailsMapper.Problem([err.Error]));

				return response;
			})
            .WithTags(Tags.Players)
            .MapToApiVersion(1)
			.WithSummary("Create booking")
			.WithDescription("Create booking for a player");
	}
}
