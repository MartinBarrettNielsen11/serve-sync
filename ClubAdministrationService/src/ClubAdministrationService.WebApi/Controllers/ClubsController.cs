using ClubAdministrationService.Contracts.Clubs;
using SharedKernel.Results;
using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Application.Clubs.ListClubs;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClubAdministrationService.WebApi.Controllers;

/// <summary>
/// This has to be replaced
/// </summary>
/// <param name="sender"></param>
[Route("subscriptions/{subscriptionId:guid}/clubs")]
public sealed class ClubsController(ISender sender) : ApiController
{
    /*
    /// <summary> Creates a new club </summary>
    /// <remarks>
    /// Creates a club for the specified subscription.
    /// The caller must have permission to create clubs within the subscription.
    /// </remarks>
    /// <param name="request"> The club creation request containing the club name, description, and settings </param>
    /// <param name="subscriptionId"> The subscription identifier that owns the club </param>
    /// <param name="cancellationToken"></param>
    /// <returns> The newly created club </returns>
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
    */
    
    
    
    /// <summary>
    /// returns all clubs for a particular subscription
    /// </summary>
    /// <param name="subscriptionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ListClubs(Guid subscriptionId, CancellationToken cancellationToken)
    {
        ListClubsQuery command = new(subscriptionId);

        Result<List<Club>> listGymsResult = await sender.Send(command, cancellationToken);

        return listGymsResult.Match(
            onSuccess: clubs => Ok(clubs.ConvertAll(c => new ClubResponse(c.Id, c.Name))),
            onFailure: errors => Problem([errors]));
    }
}