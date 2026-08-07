using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Queries.ListClubs;

internal sealed class ListClubsQueryHandler(
	IClubsRepository clubsRepository,
	ISubscriptionsRepository subscriptionsRepository)
	: IRequestHandler<ListClubsQuery, Result<List<Club>>>
{
	public async ValueTask<Result<List<Club>>> Handle(ListClubsQuery query, CancellationToken cancellationToken)
	{
		var subscriptionExists = await subscriptionsRepository.ExistsAsync(query.SubscriptionId, cancellationToken);

		if (!subscriptionExists)
		{
			return Result.Failure<List<Club>>(Error.NotFound("", "Subscription not found"));
		}

		List<Club> clubs = await clubsRepository.ListSubscriptionClubs(query.SubscriptionId, cancellationToken);

		return clubs;
	}
}
