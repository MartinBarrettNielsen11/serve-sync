using System.Reflection;
using Scalar.AspNetCore;
using SessionBookingService.Application;
using SessionBookingService.Infrastructure;
using SessionBookingService.Infrastructure.Middlewares;
using SessionBookingService.WebApi.Extensions;

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
    .AddMediatorServices()
    .AddServices()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints();


// app.UseMiddleware<EventualConsistencyMiddleware>(); // I'll need this back some day

// Configure the HTTP request pipeline for DEVELOPMENT only
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference((opts) =>
    {
        opts.Title = Assembly.GetExecutingAssembly().GetName().Name!;
        opts.Theme = ScalarTheme.DeepSpace;
    });
}

app.MapControllers();
await app.RunAsync();