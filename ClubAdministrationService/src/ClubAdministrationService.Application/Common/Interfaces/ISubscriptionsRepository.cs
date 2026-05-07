using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<bool> ExistsAsync(Guid id);
    Task<Subscription?> GetByIdAsync(Guid id);
    Task UpdateAsync(Subscription subscription);
}