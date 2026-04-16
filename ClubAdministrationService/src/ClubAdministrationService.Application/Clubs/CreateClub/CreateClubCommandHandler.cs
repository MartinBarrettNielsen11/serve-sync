using System.Runtime.CompilerServices;
using SharedKernel.Results;
using ClubAdministrationService.Domain.ClubAggregate;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed class CreateClubCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal async ValueTask<Result> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        
        var yo = new Club(command.Name, 1, Guid.NewGuid());

    }
   
}