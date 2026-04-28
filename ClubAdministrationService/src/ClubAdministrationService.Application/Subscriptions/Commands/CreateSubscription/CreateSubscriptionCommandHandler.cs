using System.Runtime.CompilerServices;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;

// ReSharper disable once UnusedType.Global
internal sealed class CreateSubscriptionCommandHandler
{
#pragma warning disable S1186
#pragma warning disable CA1822
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result<Subscription>>))]
    internal async Task<Result<Subscription>> Handle(CreateSubscriptionCommand command,
#pragma warning restore CA1822
#pragma warning restore S1186
        CancellationToken cancellationToken)
    {
        return null!;
    }
}