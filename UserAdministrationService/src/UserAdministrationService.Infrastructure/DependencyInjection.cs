using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using UserAdministrationService.Application.Interfaces;
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
        JwtOptions jwtSettings = new();
        configuration.Bind(JwtOptions.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}