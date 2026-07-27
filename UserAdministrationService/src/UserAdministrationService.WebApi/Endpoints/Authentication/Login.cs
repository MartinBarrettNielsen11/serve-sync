using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Login;
using UserAdministrationService.Contracts.Authentication;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;

namespace UserAdministrationService.WebApi.Endpoints.Authentication;

public sealed class Login : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("login",
				async (LoginRequest request,
					   ISender sender,
					   CancellationToken cancellationToken) =>
				{
					LoginQuery query = new(request.Email, request.Password);
					Result<AuthenticationResult> authResult = await sender.Send(query, cancellationToken);

					if (authResult.IsFailure && authResult.Error == AuthenticationErrors.InvalidCredentials)
					{
						return Results.Unauthorized();
					}

					IResult response = authResult.Match(
						onSuccess: p => Results.Ok(new AuthenticationResponse(p.User.Id, "", "", "", "")),
						onFailure: p => ProblemDetailsMapper.Problem([p.Error]));

					return response;
				})
			.WithTags(Tags.Authentication)
			.WithSummary("Login")
			.WithDescription("Login")
			.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
