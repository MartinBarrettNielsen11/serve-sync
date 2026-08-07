using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Sessions.Queries.GetSession;

internal sealed class GetSessionQueryHandler(ISessionsRepository sessionsRepository,
											ICourtsRepository courtsRepository)
	: IRequestHandler<GetSessionQuery, Result<Session>>
{
	public async ValueTask<Result<Session>> Handle(GetSessionQuery query, CancellationToken cancellationToken)
	{
		Court? court = await courtsRepository.GetByIdAsync(query.CourtId, cancellationToken);

		if (court is null)
		{
			return Result.Failure<Session>(Error.Failure("CourtNotFound", "Court not found"));
		}

		var hasSession = court.HasSession(query.SessionId);
		if (!hasSession)
		{
			return Result.Failure<Session>(Error.Failure("SessionNotFound", "Session not found"));
		}

		Session? session = await sessionsRepository.GetByIdAsync(query.SessionId, cancellationToken);
		if (session is null)
		{
			return Result.Failure<Session>(Error.NotFound("SessionNotFound", "Session not found"));
		}

		return Result.Success(session);
	}
}
