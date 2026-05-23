using System.Diagnostics.CodeAnalysis;
using ClubAdministrationService.Contracts.Clubs;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Controllers;

using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[Route("subscriptions/{subscriptionId:guid}/clubs")]
public sealed class ClubsController(ISender sender) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateClub(CreateClubRequest request, Guid subscriptionId, CancellationToken cancellationToken)
    {
        CreateClubCommand command = new(request.Name, subscriptionId);

        Result<Club> createClubResult = await sender.Send(command, cancellationToken);

        IActionResult response = createClubResult.Match(
            onSuccess: c => CreatedAtAction(actionName: "yo_mama",
                                            routeValues: new { subscriptionId, ClubId = c.Id },
                                            value: new ClubResponse(c.Id, c.Name)),
            onFailure: errors => Problem([errors]));

        return response;
    }
}