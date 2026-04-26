using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAdministrationService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services,  IConfiguration config)
    {
        throw new NotSupportedException();
    }
}