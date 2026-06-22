using SessionBookingService.Domain.PlayerAggregate;

namespace SessionBookingService.Application.Common;

internal interface IPlayersRepository
{
    Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddPlayerAsync(Player player, CancellationToken cancellationToken);
    Task UpdateAsync(Player player, CancellationToken cancellationToken);
    Task UpdateRangeAsync(ICollection<Player> players, CancellationToken cancellationToken);
}