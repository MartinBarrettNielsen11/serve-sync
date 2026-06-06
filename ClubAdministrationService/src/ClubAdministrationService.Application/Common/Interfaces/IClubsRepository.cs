using ClubAdministrationService.Domain.ClubAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface IClubsRepository
{
    Task AddClubAsync(Club club);
    Task<Club?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateAsync(Club club);
    Task<List<Club>> ListSubscriptionClubs(Guid subscriptionId);
}