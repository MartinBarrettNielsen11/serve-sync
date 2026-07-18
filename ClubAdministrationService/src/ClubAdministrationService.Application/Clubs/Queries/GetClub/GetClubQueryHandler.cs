using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Queries.GetClub;

internal sealed class GetClubQueryHandler(
	IClubsRepository clubsRepository,
	ISubscriptionsRepository subscriptionsRepository)
	: IRequestHandler<GetClubQuery, Result<Club>>
{
	public async ValueTask<Result<Club>> Handle(GetClubQuery request, CancellationToken cancellationToken)
	{
		if (await subscriptionsRepository.ExistsAsync(request.SubscriptionId, cancellationToken))
		{
			return Result.Failure<Club>(Error.NotFound("", "Subscription not found"));
		}

		if (await clubsRepository.GetByIdAsync(request.GymId, cancellationToken) is not Club club)
		{
			return Result.Failure<Club>(Error.NotFound("", "Club not found"));
		}

		return club;
	}
}
