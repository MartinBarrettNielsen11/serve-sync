using Microsoft.Extensions.DependencyInjection;

namespace ClubAdministrationService.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        return services;
    }
}