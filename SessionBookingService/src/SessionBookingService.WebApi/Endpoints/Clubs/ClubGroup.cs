using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Clubs;

internal static class ClubGroup
{
	internal static RouteGroupBuilder MapBookingGroup(this IEndpointRouteBuilder app, ApiVersionSet versionSet)
	{
		return app.MapGroup("api/v{version:apiVersion}")
			.WithApiVersionSet(versionSet)
			.MapToApiVersion(1)
			.WithTags(Tags.Clubs);
	}
}
