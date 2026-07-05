using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

// ReSharper disable once UnusedType.Global
internal sealed class DeleteCourtCommandHandler(IClubsRepository clubsRepository)
	: IRequestHandler<DeleteCourtCommand, Result>
{
	public async ValueTask<Result> Handle(DeleteCourtCommand command, CancellationToken cancellationToken)
	{
		Club? club = await clubsRepository.GetByIdAsync(command.ClubId, cancellationToken);

		if (club is null) return Result.Failure(Error.NotFound("ClubNotFound", "Club not found"));

		if (!club.HasCourt(command.CourtId)) return Result.Failure(Error.NotFound("CourtNotFound", "Court not found"));

		club.RemoveCourt(command.CourtId);

		await clubsRepository.UpdateAsync(club, cancellationToken);

		return Result.Success(true);
	}
}