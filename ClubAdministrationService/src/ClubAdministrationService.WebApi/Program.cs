using ClubAdministrationService.Application;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Infrastructure.Middleware;
using ClubAdministrationService.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

app.MapOpenApi().AllowAnonymous();

app.MapScalarApiReference((opts) =>
{
    opts.Title = "ClubAdministrationService.WebApi";
    opts.Theme = ScalarTheme.Kepler;
}).AllowAnonymous();


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
#pragma warning disable MA0042
#pragma warning disable CA1849
app.Run();
#pragma warning restore CA1849
#pragma warning restore MA0042
