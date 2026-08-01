using Mediator;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreateInstructorProfile;

public sealed record CreateInstructorProfileCommand(Guid UserId) : IRequest<Result<Guid>>;
