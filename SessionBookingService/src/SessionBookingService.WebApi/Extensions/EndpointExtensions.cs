using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SessionBookingService.WebApi.Endpoints;
using SessionBookingService.WebApi.Endpoints.Bookings;
using SessionBookingService.WebApi.Endpoints.Players;

namespace SessionBookingService.WebApi.Extensions;

internal static class EndpointExtensions
{
    public static void AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);
    }

    public static void MapEndpoints(this WebApplication app)
    {
        ApiVersionSet versionSet = app
            .NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();
        
        RouteGroupBuilder bookingGroup = app.MapBookingGroup(versionSet);
        RouteGroupBuilder playerGroup = app.MapPlayerGroup(versionSet);
        
        new CreateBooking().MapEndpoint(bookingGroup);
        new CancelBooking().MapEndpoint(playerGroup);
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission) =>
        app.RequireAuthorization(permission);
}
