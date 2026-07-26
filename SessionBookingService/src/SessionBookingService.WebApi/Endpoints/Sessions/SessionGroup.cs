using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Sessions;

internal static class SessionGroup
{
	internal static RouteGroupBuilder MapPlayerGroup(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
	{
		return app.MapGroup("api/v{version:apiVersion}")
			.WithApiVersionSet(versionSet)
			.MapToApiVersion(1)
			.WithTags(Tags.Sessions);
	}
}
