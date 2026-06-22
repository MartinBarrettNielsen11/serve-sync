using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.CreateCourt;

internal sealed class CreateCourtCommandHandler(IClubsRepository clubsRepository, ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<CreateCourtCommand, Result<Court>>
{
    public async Task<Result<Court>> Handle(CreateCourtCommand command, CancellationToken cancellationToken)
    {
        Club? club = await clubsRepository.GetByIdAsync(command.ClubId, cancellationToken);

        if (club is null)
        {
            return Result.Failure<Court>(Error.NotFound(code: "ClubNotFound", description: "Club not found"));
        }

        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(club.SubscriptionId, cancellationToken);

        if (subscription is null)
        {
            return Result.Failure<Court>(Error.NotFound(code: "SubscriptionNotFound",
                                                        description: "Subscription not found"));
        }

        Court court = new(name: command.CourtName, 
                          clubId: club.Id,
                          maxDailySessions: subscription.GetMaxDailySessionsAllowed());

        Result<bool> addClubResult = club.AddCourt(court);

        if (addClubResult.IsFailure)
        {
            return Result.Failure<Court>(addClubResult.Error);
        }

        await clubsRepository.UpdateAsync(club, cancellationToken);

        return court;
    }
}