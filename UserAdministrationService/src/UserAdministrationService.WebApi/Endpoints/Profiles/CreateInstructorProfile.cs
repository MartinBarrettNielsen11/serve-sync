using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.CreateAdminProfile;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Profiles;

public sealed class CreateInstructorProfile : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("users/{userId:guid}/profiles/instructor",
				async (Guid userId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{

				})
			.WithTags(Tags.Profile)
			.WithSummary("Create admin profile")
			.WithDescription("Create admin profile")
			.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
