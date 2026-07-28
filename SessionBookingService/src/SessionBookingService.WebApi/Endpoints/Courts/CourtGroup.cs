using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Courts;

internal static class CourtGroup
{
	internal static RouteGroupBuilder MapCourtGroup(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
	{
		return app.MapGroup("api/v{version:apiVersion}")
			.WithApiVersionSet(versionSet)
			.MapToApiVersion(1)
			.WithTags(Tags.Courts);
	}
}
