using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Persistence.Repositories;

internal sealed class SessionsRepository(SessionBookingDbContext dbContext) : ISessionsRepository
{
    public async Task AddSessionAsync(Session session, CancellationToken cancellationToken)
    {
        await dbContext.Sessions.AddAsync(session, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken)
    {
        dbContext.Update(session);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(Session session, CancellationToken cancellationToken)
    {
        dbContext.Sessions.Remove(session);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}