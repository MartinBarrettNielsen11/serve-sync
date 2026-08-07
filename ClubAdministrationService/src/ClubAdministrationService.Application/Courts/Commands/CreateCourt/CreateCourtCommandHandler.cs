using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.CourtAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.CreateCourt;

internal sealed class CreateCourtCommandHandler(IClubsRepository clubsRepository,
												ISubscriptionsRepository subscriptionsRepository)
	: IRequestHandler<CreateCourtCommand, Result<Court>>
{
	public async ValueTask<Result<Court>> Handle(
		CreateCourtCommand command,
		CancellationToken cancellationToken)
	{
		Club? club = await clubsRepository.GetByIdAsync(command.ClubId,
														cancellationToken);

		if (club is null)
		{
			return Result.Failure<Court>(Error.NotFound("ClubNotFound", "Club not found"));
		}

		Subscription? subscription =
			await subscriptionsRepository.GetByIdAsync(club.SubscriptionId,
														cancellationToken);

		if (subscription is null)
		{
			return Result.Failure<Court>(Error.NotFound("SubscriptionNotFound",
														"Subscription not found"));
		}

		Court court = new(command.CourtName,
						club.Id,
						subscription.GetMaxDailySessionsAllowed());

		Result<bool> addCourtResult = club.AddCourt(court);

		if (addCourtResult.IsFailure)
		{
			return Result.Failure<Court>(addCourtResult.Error);
		}

		await clubsRepository.UpdateAsync(club, cancellationToken);

		return Result.Success(court);
	}
}
