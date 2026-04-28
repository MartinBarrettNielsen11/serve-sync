namespace ClubAdministrationService.Application.Courts.Commands.CreateCourt;

internal sealed record CreateCourtCommand(Guid ClubId, string CourtName);