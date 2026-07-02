using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SessionBookingService.WebApi.Endpoints;
using SessionBookingService.WebApi.Endpoints.Bookings;

namespace SessionBookingService.WebApi.Extensions;

internal static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        RouteGroupBuilder bookings = app.MapBookingGroup();
        
        new CreateBooking().MapEndpoint(bookings);
        
        return app;
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission) =>
        app.RequireAuthorization(permission);
}
