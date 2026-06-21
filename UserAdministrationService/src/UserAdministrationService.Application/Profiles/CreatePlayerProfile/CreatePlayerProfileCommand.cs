using MediatR;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreatePlayerProfile;

internal sealed record CreatePlayerProfileCommand(Guid UserId) : IRequest<Result<Guid>>;