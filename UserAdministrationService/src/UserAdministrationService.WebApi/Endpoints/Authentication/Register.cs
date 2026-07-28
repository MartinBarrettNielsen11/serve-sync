using Mediator;
using SharedKernel.Results;
using UserAdministrationService.Application.Common;
using UserAdministrationService.Application.Register;
using UserAdministrationService.Contracts.Authentication;
using UserAdministrationService.Contracts.Profiles;
using UserAdministrationService.WebApi.Infrastructure;
using RegisterRequest = UserAdministrationService.Contracts.Authentication.RegisterRequest;

namespace UserAdministrationService.WebApi.Endpoints.Authentication;

public sealed class Register : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("register", async (RegisterRequest request, ISender sender, CancellationToken cancellationToken) =>
			{
				RegisterCommand command = new(request.FirstName, request.LastName, request.Email, request.Password);

				Result<AuthenticationResult> authResult = await sender.Send(command, cancellationToken);

                IResult response = authResult.Match(
                    onSuccess: p => Results.Ok(new AuthenticationResponse(p.User.Id, "", "", "", "")),
                    onFailure: p => ProblemDetailsMapper.Problem([p.Error]));

				return response;
			})
		.WithTags(Tags.Authentication)
		.WithSummary("Register")
		.WithDescription("Register")
		.Produces<ProfileResponse>(StatusCodes.Status201Created);
	}
}
