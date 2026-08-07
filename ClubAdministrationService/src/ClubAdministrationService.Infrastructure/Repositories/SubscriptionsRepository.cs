using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministrationService.Infrastructure.Repositories;

internal sealed class SubscriptionsRepository(ClubDbContext clubDbContext) : ISubscriptionsRepository
{
	public async Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
	{
		await clubDbContext.Subscriptions.AddAsync(subscription, cancellationToken);
		await clubDbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
	{
		return await clubDbContext.Subscriptions
								.AsNoTracking()
								.AnyAsync(s => s.Id == id, cancellationToken);
	}

	public async Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await clubDbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
	}

	public async Task<List<Subscription>> ListAsync(CancellationToken cancellationToken)
	{
		return await clubDbContext.Subscriptions.ToListAsync(cancellationToken);
	}

	public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken)
	{
		clubDbContext.Update(subscription);
		await clubDbContext.SaveChangesAsync(cancellationToken);
	}
}
