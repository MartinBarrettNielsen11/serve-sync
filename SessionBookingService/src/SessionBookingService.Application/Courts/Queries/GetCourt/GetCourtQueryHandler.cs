using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Courts.Queries.GetCourt;

internal sealed class GetCourtQueryHandler(ICourtsRepository roomsRepository)
	: IRequestHandler<GetCourtQuery, Result<Court>>
{
	public async ValueTask<Result<Court>> Handle(GetCourtQuery query, CancellationToken cancellationToken)
	{
		return await roomsRepository.GetByIdAsync(query.CourtId, cancellationToken) is not Court court
			? Result.Failure<Court>(Error.NotFound("", "Room not found"))
			: Result.Success<Court>(court);
	}
}
