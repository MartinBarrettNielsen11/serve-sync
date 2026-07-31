using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Queries.GetClub;

public sealed record GetClubQuery(Guid SubscriptionId, Guid GymId) : IRequest<Result<Club>>;
