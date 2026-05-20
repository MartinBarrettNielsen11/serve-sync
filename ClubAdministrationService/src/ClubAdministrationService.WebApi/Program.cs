using ClubAdministrationService.Application;
using ClubAdministrationService.Domain.ClubAggregate;
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
        
        if (!await dbContext.Clubs.AnyAsync())
        {
            var clubs = new List<Club>
            {
                new(
                    name: "Vibenhuset tennis club",
                    maxCourtCapacity: 5,
                    subscriptionId: Guid.CreateVersion7()
                ),
                new(
                    name: "Nørrebro tennis club",
                    maxCourtCapacity: 4,
                    subscriptionId: Guid.CreateVersion7()
                )
            };

            dbContext.Clubs.AddRange(clubs);
            await dbContext.SaveChangesAsync();
        }
        
    }
    
    app.MapControllers();
    await app.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    throw;
}