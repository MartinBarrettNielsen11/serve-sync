using ClubAdministrationService.Application.Subscriptions.Commands.CreateSubscription;
using ClubAdministrationService.Contracts.Subscriptions;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Results;
using SubscriptionType = ClubAdministrationService.Domain.SubscriptionAggregate.SubscriptionType;

namespace ClubAdministrationService.WebApi.Controllers;

[Route("subscriptions")]
public sealed class SubscriptionsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request, CancellationToken cancellationToken)
    {
        if (!SubscriptionType.TryFromName(request.SubscriptionType.ToString(), out SubscriptionType? subscriptionType))
        {
            return Problem("Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
        }

        CreateSubscriptionCommand command = new(subscriptionType, request.AdminId);

        Result<Subscription> createSubscriptionResult = await sender.Send(command, cancellationToken);
        
        IActionResult response = createSubscriptionResult.Match(
            onSuccess: s => Ok(new SubscriptionResponse(s.Id, ToDto(s.SubscriptionType))),
            onFailure: errors => Problem([errors]));

        return response;
    }
    
   
    private static Contracts.Subscriptions.SubscriptionType ToDto(SubscriptionType subscriptionType)
    {
       return subscriptionType.Name switch
       {
           nameof(SubscriptionType.Free) => Contracts.Subscriptions.SubscriptionType.Free,
           nameof(SubscriptionType.Starter) => Contracts.Subscriptions.SubscriptionType.Starter,
           nameof(SubscriptionType.Pro) => Contracts.Subscriptions.SubscriptionType.Pro,
           _ => throw new InvalidOperationException(),
       };
    }
}