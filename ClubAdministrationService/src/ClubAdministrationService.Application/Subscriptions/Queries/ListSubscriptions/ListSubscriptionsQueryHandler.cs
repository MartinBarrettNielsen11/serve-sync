using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Subscriptions.Queries.ListSubscriptions;

internal class ListSubscriptionsQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    : IRequestHandler<ListSubscriptionsQuery, Result<List<Subscription>>>
{
    public async ValueTask<Result<List<Subscription>>> Handle(ListSubscriptionsQuery request, 
                                                              CancellationToken cancellationToken)
    {
        return await subscriptionsRepository.ListAsync(cancellationToken);
    }
}
