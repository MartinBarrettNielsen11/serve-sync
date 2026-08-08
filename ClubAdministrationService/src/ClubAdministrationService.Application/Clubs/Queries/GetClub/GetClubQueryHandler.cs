using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Queries.GetClub;

internal sealed class GetClubQueryHandler(IClubsRepository clubsRepository,
										  ISubscriptionsRepository subscriptionsRepository)
	: IRequestHandler<GetClubQuery, Result<Club>>
{
	public async ValueTask<Result<Club>> Handle(GetClubQuery request, CancellationToken cancellationToken)
	{
		var exists = await subscriptionsRepository.ExistsAsync(request.SubscriptionId, cancellationToken);
		if (!exists)
		{
			return Result.Failure<Club>(Error.NotFound(code: "SubscriptionNotFound",
													   description: "Subscription not found"));
		}

		Club? club = await clubsRepository.GetByIdAsync(request.ClubId, cancellationToken);
		if (club is null)
		{
			return Result.Failure<Club>(Error.NotFound(code: "ClubNotFound", description: "Club not found"));
		}

		return club;
	}
}
