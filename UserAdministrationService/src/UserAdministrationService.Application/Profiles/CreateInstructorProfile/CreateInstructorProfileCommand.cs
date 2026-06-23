using Mediator;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.CreateInstructorProfile;

public record CreateInstructorProfileCommand(Guid UserId) : IRequest<Result<Guid>>;