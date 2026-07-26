using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Infrastructure.Authentication;
using UserAdministrationService.Infrastructure.Repositories;

namespace UserAdministrationService.Infrastructure;

internal static class DependencyInjection
{
	internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
	{
		services
			.AddAuthentication(config)
			.AddPersistence(config);

		return services;
	}


	internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		JwtOptions jwtSettings = new();
		configuration.Bind(JwtOptions.Section, jwtSettings);

		services.AddSingleton(Options.Create(jwtSettings));
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

		services.AddSingleton<IPasswordHasher, PasswordHasher>();

		return services;
	}

	internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
	{
		var connectionString = config.GetConnectionString("Database");

		services.AddDbContext<UserDbContext>(options => { options.UseNpgsql(connectionString); });

		services.AddScoped<IUsersRepository, UsersRepository>();
		services.AddHttpContextAccessor();

		return services;
	}
}
