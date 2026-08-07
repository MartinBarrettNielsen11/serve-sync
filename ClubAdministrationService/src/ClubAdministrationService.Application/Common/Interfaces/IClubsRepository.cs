using ClubAdministrationService.Domain.ClubAggregate;

namespace ClubAdministrationService.Application.Common.Interfaces;

internal interface IClubsRepository
{
	Task AddClubAsync(Club club, CancellationToken cancellationToken);
	Task<Club?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
	Task UpdateAsync(Club club, CancellationToken cancellationToken);
	Task<List<Club>> ListSubscriptionClubs(Guid subscriptionId, CancellationToken cancellationToken);
}
