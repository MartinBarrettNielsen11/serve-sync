using ClubAdministrationService.Domain.ClubAggregate;
using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.GetClub;

public record GetClubQuery(Guid SubscriptionId, Guid GymId) : IRequest<Result<Club>>;
