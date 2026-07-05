using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.ListProfiles;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Profiles;

public sealed class ListProfiles : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("users/{userId:guid}/profiles",
				async (Guid userId,
					ISender sender,
					CancellationToken cancellationToken) =>
				{
					ListProfilesQuery listProfilesQuery = new(userId);

					Result<ListProfilesResult> listProfilesResult =
						await sender.Send(listProfilesQuery, cancellationToken);

					IResult result = listProfilesResult.Match(
						profiles => Results.Ok(new ListProfilesResponse(
							profiles.AdminId,
							profiles.PlayerId,
							profiles.InstructorId)),
						errors => ProblemDetailsMapper.Problem([errors.Error]));

					return result;
				})
			.WithTags(Tags.Profile)
			.WithSummary("Get profiles for user")
			.WithDescription("Get profiles for user")
			.Produces<ListProfilesResponse>();
	}
}