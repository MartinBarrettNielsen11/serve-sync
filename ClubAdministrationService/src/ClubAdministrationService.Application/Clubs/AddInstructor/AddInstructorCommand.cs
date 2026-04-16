namespace ClubAdministrationService.Application.Clubs.AddInstructor;

internal sealed record AddInstructorCommand(Guid SubscriptionId, Guid ClubId, Guid InstructorId);