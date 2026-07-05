using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface ISubscriptionsRepository
{
	Task AddSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken);
	Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
	Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken);
	Task<List<Subscription>> ListAsync(CancellationToken cancellationToken);
}