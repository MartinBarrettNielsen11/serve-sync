using ClubAdministrationService.Application.Admins.IntegrationEvents;
using ClubAdministrationService.Application.Courts.Commands.CreateCourt;
using Mediator;
using SharedKernel.IntegrationEvents.ClubManagement;
using SharedKernel.IntegrationEvents.UserManagement;

namespace ClubAdministrationService.WebApi.Extensions;

internal static class MediatorExtensions
{
	internal static IServiceCollection AddMediatorServices(this IServiceCollection services)
	{
		services.AddMediator(options =>
		{
			options.ServiceLifetime = ServiceLifetime.Scoped;
			options.GenerateTypesAsInternal = true;
			options.NotificationPublisherType = typeof(ForeachAwaitPublisher);
			options.CachingMode = CachingMode.Eager;
			options.Assemblies =
			[
				typeof(AdminProfileCreatedEventHandler).Assembly
			];
		});

		return services;
	}
}
