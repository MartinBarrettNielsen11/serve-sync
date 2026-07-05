using Mediator;
using UserAdministrationService.Infrastructure.IntegrationEvents.OutboxWriter;

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
			// options.CachingMode = CachingMode.Eager;
			options.Assemblies =
			[
				typeof(OutboxWriterEventHandler).Assembly
			];
		});

		return services;
	}
}