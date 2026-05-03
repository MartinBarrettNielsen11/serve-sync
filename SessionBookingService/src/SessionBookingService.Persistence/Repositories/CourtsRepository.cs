using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.Persistence.Repositories;

internal class CourtsRepository(SessionBookingDbContext dbContext) : ICourtsRepository
{
    public async Task AddCourtAsync(Court court)
    {
        await dbContext.Courts.AddAsync(court);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Court?> GetByIdAsync(Guid id)
    {
        return await dbContext.Courts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateAsync(Court court)
    {
        dbContext.Courts.Update(court);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Court court)
    {
        dbContext.Courts.Remove(court);
        await dbContext.SaveChangesAsync();
    }
}