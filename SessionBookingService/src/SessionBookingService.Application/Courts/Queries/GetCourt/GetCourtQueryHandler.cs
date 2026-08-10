using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Courts.Queries.GetCourt;

internal sealed class GetCourtQueryHandler(ICourtsRepository courtsRepository)
	: IRequestHandler<GetCourtQuery, Result<Court>>
{
	public async ValueTask<Result<Court>> Handle(GetCourtQuery query, CancellationToken cancellationToken)
	{
		Court? court = await courtsRepository.GetByIdAsync(query.CourtId, cancellationToken);

		if (court is null)
		{
			return Result.Failure<Court>(Error.NotFound(code: "", description: "Court not found"));
		}

		return Result.Success(court);
	}
}
