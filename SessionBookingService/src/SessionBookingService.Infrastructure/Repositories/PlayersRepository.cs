using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.PlayerAggregate;

namespace SessionBookingService.Infrastructure.Repositories;

internal sealed class PlayersRepository(SessionBookingDbContext dbContext) : IPlayersRepository
{
    public async Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Players.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    
    public async Task AddPlayerAsync(Player player, CancellationToken cancellationToken)
    {
        await dbContext.Players.AddAsync(player, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Player player, CancellationToken cancellationToken)
    {
        dbContext.Update(player);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(ICollection<Player> players, CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(players);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}