using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

// ReSharper disable once UnusedType.Global
internal sealed class DeleteCourtCommandHandler(IClubsRepository clubsRepository)
    : IRequestHandler<DeleteCourtCommand, Result>
{
    public async Task<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
    {
        Club? club = await clubsRepository.GetByIdAsync(command.ClubId);

        if (club is null)
        {
            return Result.Failure(Error.NotFound(code: "ClubNotFound", description: "Club not found"));
        }

        if (!club.HasCourt(command.CourtId))
        {
            return Result.Failure(Error.NotFound(code: "CourtNotFound", description: "Court not found"));
        }
        
        club.RemoveCourt(command.CourtId);
        
        await clubsRepository.UpdateAsync(club);

        return Result.Success<bool>(value: true);
    }
}
