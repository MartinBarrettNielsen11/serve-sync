using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SessionBookingService.Domain.PlayerAggregate;

namespace SessionBookingService.Application.Common;

internal interface IPlayersRepository
{
    Task<Player?> GetByIdAsync(Guid id);
    Task AddPlayerAsync(Player player);
    Task UpdateAsync(Player player);
    Task UpdateRangeAsync(ICollection<Player> players);
}