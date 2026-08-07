using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Clubs.Queries.ListSessions;

internal sealed class ListSessionsQueryHandler(ISessionsRepository sessionsRepository) :
	IRequestHandler<ListSessionsQuery, Result<List<Session>>>
{
	public async ValueTask<Result<List<Session>>> Handle(ListSessionsQuery query, CancellationToken cancellationToken)
	{
		return await sessionsRepository.ListByClubIdAsync(query.ClubId,
														query.StartDateTime,
														query.EndDateTime,
														query.Categories);
	}
}
