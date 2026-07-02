using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Players;

internal static class PlayerGroup
{
    internal static RouteGroupBuilder MapPlayerGroup(this IEndpointRouteBuilder app, ApiVersionSet versionSet) =>
        app.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .WithTags(Tags.Players);
}