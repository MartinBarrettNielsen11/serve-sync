using ClubAdministration.Persistence2;
using ClubAdministrationService.Application;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Persistence;

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
await app.RunAsync();