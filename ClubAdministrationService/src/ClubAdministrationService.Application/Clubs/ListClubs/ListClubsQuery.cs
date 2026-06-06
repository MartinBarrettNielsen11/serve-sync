using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.ListClubs;

public record ListClubsQuery(Guid SubscriptionId) : IRequest<Result<List<Club>>>;
