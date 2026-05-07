using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Persistence.Repositories;

internal class SubscriptionsRepository(ClubDbContext clubDbContext) : ISubscriptionsRepository
{
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await clubDbContext.Subscriptions.AddAsync(subscription);
        await clubDbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await clubDbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(s => s.Id == id);
    }

    public async Task<Subscription?> GetByIdAsync(Guid id)
    {
        return await clubDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await clubDbContext.Subscriptions.ToListAsync();
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        clubDbContext.Update(subscription);
        await clubDbContext.SaveChangesAsync();
    }
}