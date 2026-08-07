using Mediator;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Players.Queries.ListPlayerSessions;

public sealed record ListPlayersSessionsQuery(
	Guid PlayerId,
	DateTime? StartDateTime = null,
	DateTime? EndDateTime = null) : IRequest<Result<List<Session>>>;
