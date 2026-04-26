using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SessionBookingService.Infrastructure;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        throw new NotSupportedException();
    }
}