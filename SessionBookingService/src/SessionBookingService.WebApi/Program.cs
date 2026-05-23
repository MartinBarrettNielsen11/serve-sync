using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using SessionBookingService.Application;
using SessionBookingService.Infrastructure;
using SessionBookingService.Infrastructure.Middlewares;
using SessionBookingService.Persistence;

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