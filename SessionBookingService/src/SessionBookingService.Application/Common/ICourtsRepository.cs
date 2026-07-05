using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.Application.Common;

internal interface ICourtsRepository
{
	Task AddCourtAsync(Court court, CancellationToken cancellationToken);
	Task<Court?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
	Task UpdateAsync(Court court, CancellationToken cancellationToken);
	Task DeleteAsync(Court court, CancellationToken cancellationToken);
}