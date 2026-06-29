using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace UserAdministrationService.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        // missing something here
        services.AddMediator((MediatorOptions options) =>
        {
            options.ServiceLifetime = ServiceLifetime.Singleton;
            options.GenerateTypesAsInternal = true;
            options.NotificationPublisherType = typeof(ForeachAwaitPublisher);
            // options.Assemblies = [typeof(...)];
            // options.Types = [typeof(IModuleMarker)];
            options.PipelineBehaviors = [];
            options.StreamPipelineBehaviors = [];
            options.CachingMode = CachingMode.Eager;
        });
        
        return services;
    }
}