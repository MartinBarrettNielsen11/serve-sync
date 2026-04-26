using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserAdministrationService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services,  IConfiguration config)
    {
        throw new NotSupportedException();
    }
}