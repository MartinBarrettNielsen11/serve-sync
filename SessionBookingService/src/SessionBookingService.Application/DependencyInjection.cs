using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace SessionBookingService.Application;

internal static class DependencyInjection
{
	internal static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

		return services;
	}
}
