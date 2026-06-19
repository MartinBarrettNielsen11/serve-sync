using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.GetClub;

public record GetClubQuery(Guid SubscriptionId, Guid GymId) : IRequest<Result<Club>>;
