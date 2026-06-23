using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.DeleteCourt;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed record DeleteCourtCommand(Guid ClubId, Guid CourtId) : IRequest<Result>;