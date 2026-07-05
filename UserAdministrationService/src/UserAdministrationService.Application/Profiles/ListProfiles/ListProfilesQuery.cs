using Mediator;
using SharedKernel.Results;

namespace UserAdministrationService.Application.Profiles.ListProfiles;

public sealed record ListProfilesQuery(Guid UserId) : IRequest<Result<ListProfilesResult>>;