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
        app.MapPost(pattern: "users/{userId:guid}/profiles/admin",
                handler: async (Guid userId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    CreateAdminProfileCommand command = new(userId);

                    Result<Guid> createProfileResult = await sender.Send(command, cancellationToken);

                    IResult result = createProfileResult.Match(
                        onSuccess: id => TypedResults.CreatedAtRoute(
                            routeName: "ListProfiles",
                            routeValues: new { userId },
                            value: new ProfileResponse(id)),
                        onFailure: e => ProblemDetailsMapper.Problem([e.Error]));

                    return result;
                })
            .WithTags(Tags.Profile)
            .WithSummary("Create admin profile")
            .WithDescription("Create admin profile")
            .Produces<ProfileResponse>(StatusCodes.Status201Created);
    }
}