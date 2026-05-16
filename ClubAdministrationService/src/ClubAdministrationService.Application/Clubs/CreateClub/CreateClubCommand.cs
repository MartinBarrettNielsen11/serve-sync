using ClubAdministrationService.Domain.ClubAggregate;
using MediatR;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.CreateClub;

internal sealed record CreateClubCommand(string Name, Guid SubscriptionId) : IRequest<Result<Club>>;