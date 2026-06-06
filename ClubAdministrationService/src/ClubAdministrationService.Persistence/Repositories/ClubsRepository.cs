using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Persistence.Repositories;

internal sealed class ClubsRepository(ClubDbContext dbContext) : IClubsRepository
{
    public async Task AddClubAsync(Club club)
    {
        await dbContext.Clubs.AddAsync(club);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await dbContext.Clubs.AsNoTracking().AnyAsync(c => c.Id == id);
    }

    public async Task<Club?> GetByIdAsync(Guid id)
    {
        return await dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateAsync(Club club)
    {
        dbContext.Update(club);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Club>> ListSubscriptionClubs(Guid subscriptionId)
    {
        return await dbContext.Clubs
            .Where(c => c.SubscriptionId == subscriptionId)
            .ToListAsync();
    }
}
