using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Courts.Queries.ListCourts;

internal sealed class ListCourtsQueryHandler(ICourtsRepository courtsRepository) :
	IRequestHandler<ListCourtsQuery, Result<List<Court>>>
{
	public async ValueTask<Result<List<Court>>> Handle(ListCourtsQuery request, CancellationToken cancellationToken)
	{
		return await courtsRepository.ListByClubIdAsync(request.ClubId);
	}
}
