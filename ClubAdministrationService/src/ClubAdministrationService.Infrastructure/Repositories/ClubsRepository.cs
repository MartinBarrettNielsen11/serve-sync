using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Infrastructure.Repositories;

internal sealed class ClubsRepository(ClubDbContext dbContext) : IClubsRepository
{
	public async Task AddClubAsync(Club club, CancellationToken cancellationToken)
	{
		await dbContext.Clubs.AddAsync(club, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
	{
		return await dbContext.Clubs.AsNoTracking().AnyAsync(c => c.Id == id, cancellationToken);
	}

	public async Task<Club?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
	}

	public async Task UpdateAsync(Club club, CancellationToken cancellationToken)
	{
		dbContext.Update(club);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<List<Club>> ListSubscriptionClubs(Guid subscriptionId, CancellationToken cancellationToken)
	{
		return await dbContext.Clubs
			.Where(c => c.SubscriptionId == subscriptionId)
			.ToListAsync(cancellationToken);
	}
}
