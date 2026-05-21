using ClubAdministrationService.Application;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Infrastructure.Middleware;
using ClubAdministrationService.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((_, options) =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddServices()
    .AddPersistence(builder.Configuration);
    //.AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseMiddleware<EventualConsistencyMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (IServiceScope scope = app.Services.CreateScope())
{
    ClubDbContext dbContext = scope.ServiceProvider.GetRequiredService<ClubDbContext>();
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
