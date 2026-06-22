using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Application.Common;

internal interface ISessionsRepository
{
    Task AddSessionAsync(Session session, CancellationToken cancellationToken);
    Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Session session, CancellationToken cancellationToken);
    Task Remove(Session session, CancellationToken cancellationToken);
}