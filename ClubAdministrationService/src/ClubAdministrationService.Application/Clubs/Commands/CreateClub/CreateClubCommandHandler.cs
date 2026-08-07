using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using Microsoft.Extensions.Logging;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Commands.CreateClub;

internal sealed class CreateClubCommandHandler(ISubscriptionsRepository subscriptionsRepository,
												ILogger<CreateClubCommandHandler> logger)
	: IRequestHandler<CreateClubCommand, Result<Club>>
{
	public async ValueTask<Result<Club>> Handle(CreateClubCommand command, CancellationToken cancellationToken)
	{
		Subscription? subscription =
			await subscriptionsRepository.GetByIdAsync(command.SubscriptionId, cancellationToken);

		if (subscription is null)
		{
			return Result.Failure<Club>(Error.NotFound("SubscriptionNotFound", "Subscription not found"));
		}

		Club club = new(command.Name,
						subscription.GetMaxCourtsAllowed(),
						subscription.Id);

		Result<bool> addClubResult = subscription.AddClub(club);

		if (addClubResult.IsFailure)
		{
			return Result.Failure<Club>(addClubResult.Error);
		}

#pragma warning disable CA1848
		logger.LogInformation("Club created: {Name}", club.Name);
#pragma warning restore CA1848

		await subscriptionsRepository.UpdateAsync(subscription, cancellationToken);

		return Result.Success<Club>(club);
	}
}
