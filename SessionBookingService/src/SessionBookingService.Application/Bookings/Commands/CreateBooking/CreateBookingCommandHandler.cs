using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateBooking;

internal sealed class CreateBookingCommandHandler(
	ISessionsRepository sessionsRepository,
	IPlayersRepository playersRepository) : IRequestHandler<CreateBookingCommand, Result>
{
	public async ValueTask<Result> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
	{
		Session? session = await sessionsRepository.GetByIdAsync(command.SessionId, cancellationToken);

		if (session is null)
        {
            return Result.Failure<Guid>(Error.NotFound("SessionNotFound", "Session not found"));
        }

        if (session.HasBookingForPlayer(command.PlayerId))
        {
            return Result.Failure<Guid>(Error.Conflict("PlayerAlreadyHasBooking",
                "Player already has booking"));
        }

        Player? player = await playersRepository.GetByIdAsync(command.PlayerId, cancellationToken);

		if (player is null)
        {
            return Result.Failure<Guid>(Error.NotFound("PlayerNotFound", "Player not found"));
        }

        if (player.HasBookingForSession(session.Id))
        {
            return Result.Failure<Guid>(Error.Unexpected("PlayerNotExpectedToHaveReservationToSession",
                "Player not expected to have reservation to session"));
        }

        Result<bool> bookSpotResult = session.BookSpot(player);

		if (bookSpotResult.IsFailure)
        {
            return Result.Failure<Guid>(bookSpotResult.Error);
        }

        await sessionsRepository.UpdateAsync(session, cancellationToken);

		Result res = Result.Success();
		return res;
	}
}
