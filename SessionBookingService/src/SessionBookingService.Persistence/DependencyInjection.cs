using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SessionBookingService.Application.Common;
using SessionBookingService.Persistence.Repositories;

namespace SessionBookingService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");
        
        services.AddDbContext<SessionBookingDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IInstructorsRepository, InstructorsRepository>();
        services.AddScoped<ICourtsRepository, CourtsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<IPlayersRepository, PlayersRepository>();


        return services;
    }
}