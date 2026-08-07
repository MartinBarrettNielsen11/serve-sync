using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Application.Players.Commands.CancelBooking;

internal sealed class CancelBookingCommandHandler(IPlayersRepository playersRepository,
												ISessionsRepository sessionsRepository,
												IDateTimeProvider dateTimeProvider)
	: IRequestHandler<CancelBookingCommand, Result>
{
	public async ValueTask<Result> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
	{
		Session? session = await sessionsRepository.GetByIdAsync(command.SessionId, cancellationToken);

		if (session is null)
		{
			return Result.Failure<Result>(Error.NotFound(description: "Session not found", code: "SessionNotFound"));
		}

		if (!session.HasBookingForPlayer(command.PlayerId))
		{
			return Result.Failure<Result>(Error.NotFound(description:
														"User does not have a reservation for the given session",
														code: "BLablbla"));
		}

		Player? player = await playersRepository.GetByIdAsync(command.PlayerId, cancellationToken);

		if (player is null)
		{
			return Result.Failure<Result>(Error.NotFound(description: "Participant not found", code: "BLablbla"));
		}

		if (!player.HasBookingForSession(session.Id))
		{
			return Result.Failure<Result>(Error.Unexpected(description:
															"Participant expected to have reservation to session",
															code: "BLablbla"));
		}

		Result<bool> cancelReservationResult = session.CancelBooking(command.PlayerId, dateTimeProvider);

		if (cancelReservationResult.IsFailure)
		{
			return cancelReservationResult;
		}

		await sessionsRepository.UpdateAsync(session, cancellationToken);

		return Result.Success();
	}
}
