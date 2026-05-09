using System.Runtime.CompilerServices;
using SharedKernel.Results;
using ClubAdministrationService.Domain.ClubAggregate;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

// ReSharper disable once UnusedType.Global
internal sealed class CreateClubCommandHandler
{
    [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder<Result>))]
    internal async ValueTask<Result> Handle(CreateClubCommand command, CancellationToken cancellationToken)
    {
        //var yo = new Club(command.Name, Guid.CreateVersion7(), 1, Guid.CreateVersion7());
        return null!;
    }
   
}