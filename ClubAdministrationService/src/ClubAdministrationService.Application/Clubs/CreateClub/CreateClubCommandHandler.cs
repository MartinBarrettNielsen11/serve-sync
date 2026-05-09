using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Common.Interfaces;
using SharedKernel.Results;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

// ReSharper disable once UnusedType.Global
internal sealed class CreateClubCommandHandler(ISubscriptionsRepository subscriptionsRepository)
{
    internal async ValueTask<Result> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Result.Failure(Error.NotFound(code: "SubscriptionNotFound", description: "Subscription not found"));
        }
        
        Club club = new(name: command.Name,
                        maxCourtCapacity: subscription.GetMaxCourtsAllowed(),
                        subscriptionId: subscription.Id);

        Result<bool> addClubResult = subscription.AddClub(club);

        if (addClubResult.IsFailure)
        {
            return Result.Failure(addClubResult.Error);
        }

        await subscriptionsRepository.UpdateAsync(subscription);

        return Result.Success<Club>(club);
    }
}