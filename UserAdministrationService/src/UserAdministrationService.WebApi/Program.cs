using UserAdministrationService.Application;
using UserAdministrationService.Infrastructure;
using UserAdministrationService.Infrastructure.Middleware;
using UserAdministrationService.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

builder.Services
    .AddServices()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();
app.UseMiddleware<EventualConsistencyMiddleware>();

await app.RunAsync();