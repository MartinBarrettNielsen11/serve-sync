using ClubAdministrationService.Application.Admins.IntegrationEvents;
using Mediator;
using SharedKernel.IntegrationEvents.UserManagement;

namespace ClubAdministrationService.WebApi.Extensions;

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
            options.Assemblies =
            [
                typeof(AdminProfileCreatedEventHandler).Assembly
            ];
            options.Types =
            [
                typeof(AdminProfileCreatedIntegrationEvent)
            ];
        });

        return services;
    }
}