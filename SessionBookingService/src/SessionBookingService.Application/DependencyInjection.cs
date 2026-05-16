using System;
using Microsoft.Extensions.DependencyInjection;

namespace SessionBookingService.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
        // throw new NotSupportedException();
    }
}