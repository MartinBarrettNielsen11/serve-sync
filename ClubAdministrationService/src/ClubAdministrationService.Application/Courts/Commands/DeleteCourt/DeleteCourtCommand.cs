namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

internal sealed record DeleteCourtCommand(Guid ClubId, Guid CourtId);