using MediatR;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreateAdminProfile;

internal sealed record CreateAdminProfileCommand(Guid UserId) : IRequest<Result<Guid>>;