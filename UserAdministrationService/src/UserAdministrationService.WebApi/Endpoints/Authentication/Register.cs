using System.Security.Claims;
using Mediator;
using Microsoft.AspNetCore.Identity.Data;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Profiles.CreateInstructorProfile;
using UserAdministrationService.Application.Register;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Endpoints.Profiles;
using UserAdministrationService.WebApi.Infrastructure;
using RegisterRequest = UserAdministrationService.Contracts.Authentication.RegisterRequest;

namespace UserAdministrationService.WebApi.Endpoints.Authentication;

public sealed class Register : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("register",
			async (RegisterRequest request,
				   ISender sender,
				   CancellationToken cancellationToken) =>
			{
				/*
				RegisterCommand command = new(request.FirstName, request.LastName, request.Email, request.Password);

				Result<AuthenticationResult> authResult = await sender.Send(command, cancellationToken);

				return authResult.Match(
					authResult => base.Ok(MapToAuthResponse(authResult)),
					Problem);
				return response;
				*/
			})
		.WithTags(Tags.Authentication)
		.WithSummary("Register")
		.WithDescription("Register")
		.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
