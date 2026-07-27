using System.Security.Claims;
using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Profiles.CreatePlayerProfile;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Profiles;

public sealed class CreatePlayerProfile : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("users/{userId:guid}/profiles/player",
				async (Guid userId,
					   ClaimsPrincipal user,
					   ISender sender,
					   CancellationToken cancellationToken) =>
				{
					var requestUserIdClaim = user.FindFirstValue("id");

					if (!Guid.TryParse(requestUserIdClaim, out Guid requestUserId))
					{
						// You should somehow parse "StatusCodes.Status401Unauthorized here"
						return ProblemDetailsMapper.Problem([
							new Error(code: "UnauthorizedToCreatePlayerProfileForThisUser",
								description: "You are not authorized to create an admin profile for this user",
								type: ErrorType.Problem)]);
					}

					if (requestUserId != userId)
					{
						// You should somehow parse "StatusCodes.Status403Unauthorized here"
						return ProblemDetailsMapper.Problem([
							new Error(code: "UnauthorizedToCreatePlayerProfileForThisUser",
								description: "You are not authorized to create an player profile for this user",
								type: ErrorType.Problem)]);
					}

					CreatePlayerProfileCommand command = new(userId);

					Result<Guid> createPlayerProfileResult = await sender.Send(command, cancellationToken);

					IResult response = createPlayerProfileResult.Match(
						onSuccess: id => TypedResults.CreatedAtRoute(routeName: nameof(ListProfiles),
																	 routeValues: new { userId },
																	 value: new ProfileResponse(id)),
						onFailure: f => ProblemDetailsMapper.Problem([f.Error]));

					return response;
				})
			.WithTags(Tags.Profile)
			.WithSummary("Create player profile")
			.WithDescription("Create player profile")
			.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
