using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

// ReSharper disable once UnusedType.Global
internal sealed class CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository) : IRequestHandler<CreateSubscriptionCommand, Result<Subscription>>
{
    public async Task<Result<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        Admin? admin = await adminsRepository.GetByIdAsync(adminId: command.AdminId);

        if (admin is null)
        {
            return Result.Failure<Subscription>(Error.NotFound(code: "AdminNotFound", description: "Admin not found"));
        }

        if (admin.SubscriptionId is not null)
        {
            return Result.Failure<Subscription>(Error.Conflict(code: "AdminAlreadyHasActiveSubscription",
                                                               description: "Admin already has active subscription"));
        }
        
        Subscription subscription = new(subscriptionType: command.SubscriptionType, id: command.AdminId);
        admin.SetSubscription(subscription);
        
        await adminsRepository.UpdateAsync(admin);

        return subscription;
    }
}