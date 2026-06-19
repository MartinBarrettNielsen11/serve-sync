using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.GetClub;

internal sealed class GetClubQueryHandler : IRequestHandler<GetClubQuery, Result<Club>>
{
    private readonly IClubsRepository _clubsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetClubQueryHandler(IClubsRepository clubsRepository, ISubscriptionsRepository subscriptionsRepository)
    {
        _clubsRepository = clubsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<Result<Club>> Handle(GetClubQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
        {
            
            return Result.Failure<Club>(Error.NotFound(code: "", description: "Subscription not found"));
        }

        if (await _clubsRepository.GetByIdAsync(request.GymId) is not Club club)
        {
            return Result.Failure<Club>(Error.NotFound(code: "", description: "Club not found"));
        }

        return club;
    }
}