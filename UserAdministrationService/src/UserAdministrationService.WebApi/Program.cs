using Scalar.AspNetCore;
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

app.MapOpenApi().AllowAnonymous();

app.MapScalarApiReference((opts) =>
{
    opts.Title = "ClubAdministrationService.WebApi";
    opts.Theme = ScalarTheme.Kepler;
}).AllowAnonymous();

await app.RunAsync();