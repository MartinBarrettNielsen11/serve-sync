using Mediator;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Clubs.Queries.ListSessions;

internal sealed record ListSessionsQuery(Guid ClubId,
										DateTime? StartDateTime = null,
										DateTime? EndDateTime = null,
										List<SessionCategory>? Categories = null) : IRequest<Result<List<Session>>>;
