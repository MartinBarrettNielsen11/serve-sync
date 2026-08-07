using System.Security.Claims;
using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.CreateAdminProfile;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Profiles;

public sealed class CreateAdminProfile : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("users/{userId:guid}/profiles/admin",
				async (Guid userId, ClaimsPrincipal user, ISender sender, CancellationToken cancellationToken) =>
				{
					var requestUserIdClaim = user.FindFirstValue("id");

					if (!Guid.TryParse(requestUserIdClaim, out Guid requestUserId))
					{
						// You should somehow parse "StatusCodes.Status401Unauthorized here"
						return ProblemDetailsMapper.Problem([
							new Error("UnauthorizedToCreateAdminProfileForThisUser",
								"You are not authorized to create an admin profile for this user",
								ErrorType.Problem)
						]);
					}

					if (requestUserId != userId)
					{
						// You should somehow parse "StatusCodes.Status403Unauthorized here"
						return ProblemDetailsMapper.Problem([
							new Error("UnauthorizedToCreateAdminProfileForThisUser",
								"You are not authorized to create an admin profile for this user",
								ErrorType.Problem)
						]);
					}

					CreateAdminProfileCommand command = new(userId);

					Result<Guid> createAdminProfileResult = await sender.Send(command, cancellationToken);

					IResult result = createAdminProfileResult.Match(
						id => TypedResults.CreatedAtRoute(
							routeName: nameof(ListProfiles),
							routeValues: new { userId },
							value: new ProfileResponse(id)),
						e => ProblemDetailsMapper.Problem([e.Error]));

					return result;
				})
			.WithTags(Tags.Profile)
			.WithSummary("Create admin profile")
			.WithDescription("Create admin profile")
			.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
