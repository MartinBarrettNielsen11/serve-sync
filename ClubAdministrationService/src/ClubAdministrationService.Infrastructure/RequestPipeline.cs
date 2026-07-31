using ClubAdministrationService.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ClubAdministrationService.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}
