using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAdministrationService.Infrastructure;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration config)
    {
        throw new NotSupportedException();
    }
}