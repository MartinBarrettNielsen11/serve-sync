using Mediator;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Courts.Queries.GetCourt;

internal sealed record GetCourtQuery(Guid ClubId, Guid CourtId) : IRequest<Result<Court>>;
