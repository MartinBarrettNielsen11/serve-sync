using System.Diagnostics.CodeAnalysis;
using ClubAdministrationService.Contracts.Clubs;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Controllers;

using ClubAdministrationService.Application.Clubs.AddInstructor;
using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods")]
#pragma warning disable CA1812
[Route("subscriptions/{subscriptionId:guid}/gyms")]
internal sealed class ClubsController(ISender sender) : ApiController
#pragma warning restore CA1812
{
    // to-do: look into removing the Async suffix error only in webApi project (in a better way - no shitty pragmas)
    [HttpPost]
    public async Task<IActionResult> CreateClub(CreateClubRequest request, Guid subscriptionId, CancellationToken cancellationToken)
    {
        CreateClubCommand command = new(request.Name, subscriptionId);

        Result<Club> createClubResult = await sender.Send(command, cancellationToken);

        IActionResult? response = createClubResult.Match(
            onSuccess: club => CreatedAtAction(actionName: "yo_mama",
                routeValues: new { subscriptionId, ClubId = club.Id },
                value: new ClubResponse(club.Id, club.Name)),
            onFailure: errors => Problem([errors]));

        return response;
    }
}