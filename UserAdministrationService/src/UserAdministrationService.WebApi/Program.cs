using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UserAdministrationService.Application;
using UserAdministrationService.Infrastructure;
using UserAdministrationService.Infrastructure.Middleware;
using UserAdministrationService.WebApi.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddMediatorServices()
    .AddServices()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints();

//app.UseMiddleware<EventualConsistencyMiddleware>(); //I'll need this back some day

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
    app.MapScalarApiReference((opts) =>
    {
        opts.Title = Assembly.GetExecutingAssembly().GetName().Name!;
        opts.Theme = ScalarTheme.Kepler;
    }).AllowAnonymous();
}

/*
if (!app.Environment.IsEnvironment("Testing"))
{
    using IServiceScope scope = app.Services.CreateScope();
    UserDbContext dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    await dbContext.Database.MigrateAsync();
}*/

app.MapControllers();
await app.RunAsync();