using Mediator;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.Commands.AddInstructor;

internal sealed record AddInstructorCommand(Guid SubscriptionId, Guid ClubId, Guid InstructorId) : IRequest<Result>;