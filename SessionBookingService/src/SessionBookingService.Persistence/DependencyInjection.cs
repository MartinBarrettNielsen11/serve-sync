using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SessionBookingService.Persistence;

internal static class DependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        throw new NotSupportedException();
    }
}