using Mediator;
using SessionBookingService.Application.Courts.IntegrationEvents;
using SharedKernel.IntegrationEvents.ClubManagement;

namespace SessionBookingService.WebApi.Extensions;

internal static class MediatorExtensions
{
	internal static IServiceCollection AddMediatorServices(this IServiceCollection services)
	{
		services.AddMediator(opts =>
		{
			opts.ServiceLifetime = ServiceLifetime.Singleton;
			opts.GenerateTypesAsInternal = true;
			opts.NotificationPublisherType = typeof(ForeachAwaitPublisher);
			opts.CachingMode = CachingMode.Eager;
			opts.Assemblies =
			[
				typeof(CourtAddedEventHandler).Assembly
			];
			opts.Types =
			[
				typeof(CourtAddedIntegrationEvent)
			];
		});

		return services;
	}
}
