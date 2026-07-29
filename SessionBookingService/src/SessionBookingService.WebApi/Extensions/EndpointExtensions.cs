using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SessionBookingService.WebApi.Endpoints;
using SessionBookingService.WebApi.Endpoints.Bookings;
using SessionBookingService.WebApi.Endpoints.Courts;
using SessionBookingService.WebApi.Endpoints.Players;
using SessionBookingService.WebApi.Endpoints.Sessions;

namespace SessionBookingService.WebApi.Extensions;

internal static class EndpointExtensions
{
	public static void AddEndpoints(this IServiceCollection services, Assembly assembly)
	{
		ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } &&
							type.IsAssignableTo(typeof(IEndpoint)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
			.ToArray();

		services.TryAddEnumerable(serviceDescriptors);
	}

    public static void MapEndpoints(this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }
    }

	public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission)
	{
		return app.RequireAuthorization(permission);
	}
}
