using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Application.Common;

internal interface ISessionsRepository
{
	Task AddSessionAsync(Session session, CancellationToken cancellationToken);
	Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task UpdateAsync(Session session, CancellationToken cancellationToken);
	Task Remove(Session session, CancellationToken cancellationToken);
	Task<List<Session>> ListByIds(IReadOnlyList<Guid> sessionIds,
								DateTime? startDateTime = null,
								DateTime? endDateTime = null,
								List<SessionCategory>? categories = null);
	Task<List<Session>> ListByClubIdAsync(Guid clubId,
										 DateTime? startDateTime = null,
										 DateTime? endDateTime = null,
										 List<SessionCategory>? categories = null);
	Task<List<Session>> ListByCourtId(Guid courtId);
	Task RemoveRangeAsync(List<Session> sessions);
}
