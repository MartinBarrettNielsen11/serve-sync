using System.Reflection;
using Scalar.AspNetCore;
using SessionBookingService.Application;
using SessionBookingService.Infrastructure;
using SessionBookingService.Infrastructure.Middlewares;

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
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseMiddleware<EventualConsistencyMiddleware>();

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

await app.RunAsync();