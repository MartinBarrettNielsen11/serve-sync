using Mediator;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Sessions.Queries.GetSession;

internal sealed record GetSessionQuery(Guid CourtId, Guid SessionId) : IRequest<Result<Session>>;
