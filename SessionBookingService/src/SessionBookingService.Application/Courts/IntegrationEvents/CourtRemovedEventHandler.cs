using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;

namespace SessionBookingService.Application.Courts.IntegrationEvents;

internal sealed class CourtRemovedEventHandler
{
    private readonly ICourtsRepository _courtsRepository;

    public CourtRemovedEventHandler(ICourtsRepository courtsRepository)
    {
        _courtsRepository = courtsRepository;
    }

    public async Task Handle(CourtRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Court? court = await _courtsRepository.GetByIdAsync(notification.CourtId, cancellationToken);

        if (court is not null)
        {
            await _courtsRepository.DeleteAsync(court, cancellationToken);
        }
    }
}