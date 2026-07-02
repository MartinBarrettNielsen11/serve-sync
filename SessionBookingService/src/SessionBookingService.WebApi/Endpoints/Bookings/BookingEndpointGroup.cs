using Asp.Versioning;
using Asp.Versioning.Builder;

namespace SessionBookingService.WebApi.Endpoints.Bookings;

internal static class BookingEndpointGroup
{
    internal static RouteGroupBuilder MapBookingGroup(
        this IEndpointRouteBuilder app)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        return app.MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1)
            .WithTags(Tags.Bookings);
    }
}