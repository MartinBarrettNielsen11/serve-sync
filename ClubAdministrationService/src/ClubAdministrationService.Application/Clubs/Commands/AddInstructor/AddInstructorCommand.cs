using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.AddInstructor;

internal sealed record AddInstructorCommand(Guid SubscriptionId, Guid ClubId, Guid InstructorId) : IRequest<Result>;