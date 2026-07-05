using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.AddInstructor;

// ReSharper disable once UnusedType.Global
internal sealed class AddInstructorCommandHandler(
	IClubsRepository clubsRepository,
	ISubscriptionsRepository subscriptionsRepository)
	: IRequestHandler<AddInstructorCommand, Result>
{
	public async ValueTask<Result> Handle(AddInstructorCommand command, CancellationToken cancellationToken)
	{
		Subscription? subscription =
			await subscriptionsRepository.GetByIdAsync(command.SubscriptionId, cancellationToken);

		if (subscription is null)
			return Result.Failure(Error.NotFound("SubscriptionNotFound", "Subscription not found"));

		if (!subscription.HasClub(command.ClubId))
			return Result.Failure(Error.NotFound("ClubNotFound", "Club not found"));

		Club? club = await clubsRepository.GetByIdAsync(command.ClubId, cancellationToken);

		if (club is null) return Result.Failure(Error.NotFound("ClubNotFound", "Club not found"));

		Result<bool> addInstructorResult = club.AddInstructor(command.InstructorId);

		if (addInstructorResult.IsFailure) return Result.Failure(addInstructorResult.Error);

		await clubsRepository.UpdateAsync(club, cancellationToken);

		return Result.Success(true);
	}
}