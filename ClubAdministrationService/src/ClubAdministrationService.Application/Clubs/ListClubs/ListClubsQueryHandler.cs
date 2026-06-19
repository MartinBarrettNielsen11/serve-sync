using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.ListClubs;

internal sealed class ListClubsQueryHandler(IClubsRepository clubsRepository, ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<ListClubsQuery, Result<List<Club>>>
{
    public async Task<Result<List<Club>>> Handle(ListClubsQuery query, CancellationToken cancellationToken)
    {
        var subscriptionExists = await subscriptionsRepository.ExistsAsync(query.SubscriptionId);
        
        if (!subscriptionExists)
        {
            return Result.Failure<List<Club>>(Error.NotFound(code: "", description: "Subscription not found"));
        }
        
        List<Club> clubs = await clubsRepository.ListSubscriptionClubs(query.SubscriptionId);

        return clubs;
    }
}