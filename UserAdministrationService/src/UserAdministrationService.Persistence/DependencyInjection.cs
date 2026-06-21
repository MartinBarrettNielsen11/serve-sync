using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Persistence.Repositories;

namespace UserAdministrationService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");
        
        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}