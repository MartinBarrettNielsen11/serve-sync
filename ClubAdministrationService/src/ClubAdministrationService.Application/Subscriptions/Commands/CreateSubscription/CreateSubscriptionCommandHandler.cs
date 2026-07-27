using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

// ReSharper disable once UnusedType.Global
internal sealed class CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository)
	: IRequestHandler<CreateSubscriptionCommand, Result<Subscription>>
{
	public async ValueTask<Result<Subscription>> Handle(CreateSubscriptionCommand command,
		CancellationToken cancellationToken)
	{
		Admin? admin = await adminsRepository.GetByIdAsync(command.AdminId, cancellationToken);

        if (admin is null)
        {
            return Result.Failure<Subscription>(Error.NotFound("AdminNotFound", "Admin not found"));
        }

		if (admin.SubscriptionId is not null)
		{
			return Result.Failure<Subscription>(Error.Conflict("AdminAlreadyHasActiveSubscription",
				"Admin already has active subscription"));
		}

		Subscription subscription = new(command.SubscriptionType, command.AdminId);
		admin.SetSubscription(subscription);

		await adminsRepository.UpdateAsync(admin, cancellationToken);

		return subscription;
	}
}
