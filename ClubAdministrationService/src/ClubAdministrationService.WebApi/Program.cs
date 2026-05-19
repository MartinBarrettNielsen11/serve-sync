using ClubAdministrationService.Application;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Persistence;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddServices()
    .AddPersistence(builder.Configuration);
    //.AddInfrastructure(builder.Configuration);

try
{
    WebApplication app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ClubDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}