using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Infrastructure.Authentication;

namespace UserAdministrationService.Infrastructure;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration config)
    {
        services.AddAuthentication(config);

        return services;
    }
    
    
    internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}