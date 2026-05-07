using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

// ReSharper disable once UnusedType.Global
internal sealed class CreateSubscriptionCommandHandler(IAdminsRepository adminsRepository)
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result<Subscription>>))]
#pragma warning disable S1186
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    internal async Task<Result<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
#pragma warning restore S1186
    {
        return null!;
    }
}