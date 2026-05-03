using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Persistence.Repositories;

internal sealed class SessionsRepository(SessionBookingDbContext dbContext) : ISessionsRepository
{
    public async Task AddSessionAsync(Session session)
    {
        await dbContext.Sessions.AddAsync(session);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Session?> GetByIdAsync(Guid id)
    {
        return await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateAsync(Session session)
    {
        dbContext.Update(session);
        await dbContext.SaveChangesAsync();
    }

    public async Task Remove(Session session)
    {
        dbContext.Sessions.Remove(session);
        await dbContext.SaveChangesAsync();
    }
}