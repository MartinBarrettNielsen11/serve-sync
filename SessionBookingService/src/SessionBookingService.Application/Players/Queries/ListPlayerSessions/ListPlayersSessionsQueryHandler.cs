using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Players.Queries.ListPlayerSessions;

internal sealed class ListPlayersSessionsQueryHandler(IPlayersRepository playersRepository,
													ISessionsRepository sessionsRepository)
	: IRequestHandler<ListPlayersSessionsQuery, Result<List<Session>>>
{
	public async ValueTask<Result<List<Session>>> Handle(ListPlayersSessionsQuery query,
														CancellationToken cancellationToken)
	{
		Player? participant = await playersRepository.GetByIdAsync(query.PlayerId, cancellationToken);

		if (participant is null)
		{
			return Result.Failure<List<Session>>(Error.NotFound("PlayerNotFound", "Player not found"));
		}

		List<Session> result = await sessionsRepository.ListByIds(participant.SessionIds,
																query.StartDateTime,
																query.EndDateTime);

		return Result.Success<List<Session>>(result);
	}
}
