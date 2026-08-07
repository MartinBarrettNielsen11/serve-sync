using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionBookingService.Application.Common;
using SessionBookingService.Infrastructure.Repositories;

namespace SessionBookingService.Infrastructure;

internal static class DependencyInjection
{
	internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		services
			.AddPersistence(config);

		return services;
	}


	internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
	{
		var connectionString = config.GetConnectionString("Database");

		services.AddDbContext<SessionBookingDbContext>(options => { options.UseNpgsql(connectionString); });

		services.AddScoped<IInstructorsRepository, InstructorsRepository>();
		services.AddScoped<ICourtsRepository, CourtsRepository>();
		services.AddScoped<ISessionsRepository, SessionsRepository>();
		services.AddScoped<IPlayersRepository, PlayersRepository>();


		return services;
	}
}
