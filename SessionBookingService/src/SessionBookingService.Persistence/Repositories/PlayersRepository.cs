using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;

namespace SessionBookingService.Persistence.Repositories;

internal sealed class PlayersRepository(SessionBookingDbContext dbContext) : IPlayersRepository
{
    public async Task AddPlayerAsync(Player player)
    {
        await dbContext.Players.AddAsync(player);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Player player)
    {
        dbContext.Update(player);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(ICollection<Player> players)
    {
        dbContext.UpdateRange(players);
        await dbContext.SaveChangesAsync();
    }
}