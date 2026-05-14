using ClubAdministrationService.Contracts.Clubs;
using SharedKernel.Results;

namespace ClubAdministrationService.WebApi.Controllers;

using ClubAdministrationService.Application.Clubs.AddInstructor;
using ClubAdministrationService.Application.Clubs.CreateClub;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;


internal class ClubsController(ISender sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateClub(CreateClubRequest request, Guid subscriptionId)
    {
        CreateClubCommand command = new(request.Name, subscriptionId);

        Result<Club> createClubResult = await sender.Send(command);
        
        return createClubResult.Match(
            gym => CreatedAtAction(
                nameof(GetClub),
                new { subscriptionId, GymId = gym.Id },
                new ClubResponse(gym.Id, gym.Name)),
            Problem);
    }
}