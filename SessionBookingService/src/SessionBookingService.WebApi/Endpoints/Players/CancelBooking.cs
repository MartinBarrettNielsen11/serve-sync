using Mediator;
using Microsoft.AspNetCore.Mvc;
using SessionBookingService.Application.Players.Commands.CancelBooking;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Players;

public sealed class CancelBooking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(pattern: "{playerId:guid}/sessions/{sessionId:guid}/booking",
            handler: async (Guid playerId,
                Guid sessionId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                CancelBookingCommand command = new(playerId, sessionId);

                Result cancelBookingResult = await sender.Send(command, cancellationToken);

                IResult response = cancelBookingResult.Match(
                    onSuccess: Results.NoContent,
                    onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));
                
                return response;
            })
            .WithSummary("Cancel booking")
            .WithDescription("Cancel booking for a session for a player");
    }
}