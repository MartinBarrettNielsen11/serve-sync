using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Bookings;

internal static class BookingGroup
{
    internal static RouteGroupBuilder MapBookingGroup(this IEndpointRouteBuilder app, ApiVersionSet versionSet) =>
        app.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .WithTags(Tags.Bookings);
}