using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.Infrastructure.Repositories;

internal sealed class CourtsRepository(SessionBookingDbContext dbContext) : ICourtsRepository
{
	public async Task AddCourtAsync(Court court, CancellationToken cancellationToken)
	{
		await dbContext.Courts.AddAsync(court, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<Court?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await dbContext.Courts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
	}

	public async Task UpdateAsync(Court court, CancellationToken cancellationToken)
	{
		dbContext.Courts.Update(court);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task DeleteAsync(Court court, CancellationToken cancellationToken)
	{
		dbContext.Courts.Remove(court);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<List<Court>> ListByClubIdAsync(Guid clubId)
	{
		return await dbContext.Courts
			.AsNoTracking()
			.Where(c => c.ClubId == clubId)
			.ToListAsync();
	}
}
