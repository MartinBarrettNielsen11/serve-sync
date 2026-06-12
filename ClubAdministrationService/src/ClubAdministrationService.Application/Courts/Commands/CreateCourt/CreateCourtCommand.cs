using ClubAdministrationService.Domain.CourtAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Courts.Commands.CreateCourt;

internal sealed record CreateCourtCommand(Guid ClubId, string CourtName) : IRequest<Result<Court>>;