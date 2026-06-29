using Mediator;

namespace ClubAdministrationService.WebApi.Extensions;

internal static class MediatorExtensions
{
    internal static IServiceCollection AddMediator(this IServiceCollection services)
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