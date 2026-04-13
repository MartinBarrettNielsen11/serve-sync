using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed class CreateClubCommandHandler
{
    internal async Task<Result<Club>> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        
        var club = new Club(
            name: command.Name,
            maxCourts: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);
    }
   
}