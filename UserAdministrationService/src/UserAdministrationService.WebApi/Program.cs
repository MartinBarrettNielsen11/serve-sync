using System.Reflection;
using Scalar.AspNetCore;
using UserAdministrationService.Application;
using UserAdministrationService.Infrastructure;
using UserAdministrationService.Infrastructure.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddServices()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseMiddleware<EventualConsistencyMiddleware>();

app.MapOpenApi().AllowAnonymous();

app.MapScalarApiReference((opts) =>
{
    opts.Title = Assembly.GetExecutingAssembly().GetName().Name!;
    opts.Theme = ScalarTheme.Kepler;
}).AllowAnonymous();

await app.RunAsync();