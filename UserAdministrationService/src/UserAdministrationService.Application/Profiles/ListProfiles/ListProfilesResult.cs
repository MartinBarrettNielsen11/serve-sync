namespace UserAdministrationService.Application.Profiles.ListProfiles;

public sealed record ListProfilesResult(Guid? AdminId, Guid? PlayerId, Guid? InstructorId);
