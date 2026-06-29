using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace SessionBookingService.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
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