using System;
using System.Threading.Tasks;
using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.Application.Common;

internal interface ICourtsRepository
{
    Task AddCourtAsync(Court court);
    Task<Court?> GetByIdAsync(Guid id);
    Task UpdateAsync(Court court);
    Task DeleteAsync(Court court);
}