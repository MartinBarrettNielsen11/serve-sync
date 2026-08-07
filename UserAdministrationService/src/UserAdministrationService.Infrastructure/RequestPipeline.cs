using Microsoft.AspNetCore.Builder;
using UserAdministrationService.Infrastructure.Middleware;

namespace UserAdministrationService.Infrastructure;

internal static class RequestPipeline
{
	public static void AddInfrastructureMiddleware(this IApplicationBuilder app)
	{
		app.UseMiddleware<EventualConsistencyMiddleware>();
	}
}
