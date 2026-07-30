using Microsoft.AspNetCore.Builder;
using SessionBookingService.Infrastructure.Middlewares;

namespace SessionBookingService.Infrastructure;

internal static class RequestPipeline
{
    public static void AddInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
    }
}
