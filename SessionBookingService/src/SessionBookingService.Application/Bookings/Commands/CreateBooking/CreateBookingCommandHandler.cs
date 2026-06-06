using MediatR;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateBooking;

internal sealed class CreateBookingCommandHandler(
    ISessionsRepository sessionsRepository, 
    IPlayersRepository playersRepository)  : IRequestHandler<CreateBookingCommand, Result>
{
    public async Task<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        Session? session = await sessionsRepository.GetByIdAsync(command.SessionId);

        if (session is null)
        {
            return Result.Failure(Error.NotFound(code: "SessionNotFound", description: "Session not found"));
        }
        
        if (session.HasBookingForPlayer(command.PlayerId))
        {
            return Result.Failure(Error.Conflict(code: "PlayerAlreadyHasBooking",
                description: "Player already has booking"));
        }

        Player? player = await playersRepository.GetByIdAsync(command.PlayerId);

        if (player is null)
        {
            return Result.Failure(Error.NotFound(code: "PlayerNotFound", description: "Player not found"));
        }
        
        if (player.HasBookingForSession(session.Id))
        {
            return Result.Failure(Error.Unexpected(code: "PlayerNotExpectedToHaveReservationToSession",
                                                   description: "Player not expected to have reservation to session"));
        }
        
        Result<bool> bookSpotResult = session.BookSpot(player);

        if (bookSpotResult.IsFailure)
        {
            return Result.Failure(bookSpotResult.Error);
        }

        await sessionsRepository.UpdateAsync(session);

        Result res = Result.Success();
        return res;
    }

}