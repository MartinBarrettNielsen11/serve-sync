using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAdministrationService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");
        
        services.AddDbContext<ClubDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IClubsRepository, ClubsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();

        return services;
        
    }
}