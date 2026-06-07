using ClubAdministrationService.Application.Common.Interfaces;
using SharedKernel.Results;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed class CreateClubCommandHandler(ISubscriptionsRepository subscriptionsRepository, 
                                               ILogger<CreateClubCommandHandler> logger) 
    : IRequestHandler<CreateClubCommand, Result<Club>>
{
    public async Task<Result<Club>> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Result.Failure<Club>(Error.NotFound(code: "SubscriptionNotFound", description: "Subscription not found"));
        }
        
        Club club = new(name: command.Name,
                        maxCourtCapacity: subscription.GetMaxCourtsAllowed(),
                        subscriptionId: subscription.Id);

        Result<bool> addClubResult = subscription.AddClub(club);

        if (addClubResult.IsFailure)
        {
            return Result.Failure<Club>(addClubResult.Error);
        }
#pragma warning disable CA1848
        logger.LogInformation("Club created: {Name}", club.Name);
#pragma warning restore CA1848

        await subscriptionsRepository.UpdateAsync(subscription);

        return Result.Success<Club>(club);
    }
}