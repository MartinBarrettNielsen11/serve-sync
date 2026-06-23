using Microsoft.Extensions.DependencyInjection;

namespace UserAdministrationService.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        // missing something here
        services.AddMediator(options => options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        return services;
    }
}