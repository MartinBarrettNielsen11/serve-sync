namespace UserAdministrationService.Application.Profiles.ListProfiles;

public record ListProfilesResult(Guid? AdminId, Guid? PlayerId, Guid? InstructorId);