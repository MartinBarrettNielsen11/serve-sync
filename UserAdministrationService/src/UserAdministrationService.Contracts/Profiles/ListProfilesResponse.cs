namespace UserAdministrationService.Contracts.Profiles;

public sealed record ListProfilesResponse(Guid? AdminId, Guid? PlayerId, Guid? InstructorId);
