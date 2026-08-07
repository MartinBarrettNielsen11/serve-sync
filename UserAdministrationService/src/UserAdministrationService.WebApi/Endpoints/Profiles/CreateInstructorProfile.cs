using System.Security.Claims;
using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.CreateInstructorProfile;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Profiles;

public sealed class CreateInstructorProfile : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("users/{userId:guid}/profiles/instructor",
					async (Guid userId, ClaimsPrincipal user, ISender sender, CancellationToken cancellationToken) =>
					{
						var requestUserIdClaim = user.FindFirstValue("id");

						if (!Guid.TryParse(requestUserIdClaim, out Guid requestUserId))
						{
							// You should somehow parse "StatusCodes.Status401Unauthorized here"
							return ProblemDetailsMapper.Problem([
								new Error("UnauthorizedToCreateInstructorProfileForThisUser",
										"You are not authorized to create an admin profile for this user",
										ErrorType.Problem)
							]);
						}

						if (requestUserId != userId)
						{
							// You should somehow parse "StatusCodes.Status403Unauthorized here"
							return ProblemDetailsMapper.Problem([
								new Error("UnauthorizedToCreateInstructorProfileForThisUser",
										"You are not authorized to create an instructor profile for this user",
										ErrorType.Problem)
							]);
						}

						CreateInstructorProfileCommand command = new(userId);

						Result<Guid> createInstructorProfileResult = await sender.Send(command, cancellationToken);

						IResult response =
							createInstructorProfileResult.Match(id =>
																	TypedResults.CreatedAtRoute(routeName: nameof(
																									ListProfiles),
																								routeValues: new
																									{ userId },
																								value: new
																									ProfileResponse(
																										id)),
																f => ProblemDetailsMapper.Problem([f.Error]));

						return response;
					})
			.WithTags(Tags.Profile)
			.WithSummary("Create instructor profile")
			.WithDescription("Create instructor profile")
			.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
