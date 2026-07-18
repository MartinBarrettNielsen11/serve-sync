using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

internal sealed record DeleteCourtCommand(Guid ClubId, Guid CourtId) : IRequest<Result>;
