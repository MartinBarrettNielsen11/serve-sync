using SharedKernel.Results;
using ClubAdministrationService.Domain.ClubAggregate;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed class CreateClubCommandHandler
{
    internal async Task<Result> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        
        var yo = new Club(command.Name, 1, Guid.NewGuid());

    }
   
}