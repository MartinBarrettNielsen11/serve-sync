using Mediator;

namespace UserAdministrationService.WebApi.Extensions;

internal static class MediatorExtensions
{
    internal static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Singleton;
            options.GenerateTypesAsInternal = true;
            options.NotificationPublisherType = typeof(ForeachAwaitPublisher);
            options.CachingMode = CachingMode.Eager;
        });

        return services;
    }
}