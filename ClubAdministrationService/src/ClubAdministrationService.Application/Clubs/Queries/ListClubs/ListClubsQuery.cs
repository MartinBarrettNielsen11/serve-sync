using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Queries.ListClubs;

public sealed record ListClubsQuery(Guid SubscriptionId) : IRequest<Result<List<Club>>>;
