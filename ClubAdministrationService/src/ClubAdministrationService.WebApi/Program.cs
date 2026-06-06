using ClubAdministrationService.Application;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
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

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    builder.WebHost.UseKestrel(options => options.AddServerHeader = false);


WebApplication app = builder.Build();

app.UseMiddleware<EventualConsistencyMiddleware>();

// Configure the HTTP request pipeline for DEVELOPMENT only
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference((opts) =>
    {
        opts.Title = "ClubAdministrationService.WebApi";
        opts.Theme = ScalarTheme.DeepSpace;
    });
}



using (IServiceScope scope = app.Services.CreateScope())
{
    ClubDbContext dbContext = scope.ServiceProvider.GetRequiredService<ClubDbContext>();
    await dbContext.Database.MigrateAsync();
    
    //if (!await dbContext.Clubs.AnyAsync())
    //{
        Guid subscriptionId1 = Guid.CreateVersion7();
        Guid subscriptionId2 = Guid.CreateVersion7();
        
        var clubs = new List<Club>
        {
            new(
                name: "Vibenhuset tennis club",
                maxCourtCapacity: 5,
                subscriptionId: subscriptionId1
            ),
            new(
                name: "Nørrebro tennis club",
                maxCourtCapacity: 4,
                subscriptionId: subscriptionId2
            )
        };

        var subscriptions = new List<Subscription>
        {
            new(subscriptionType: SubscriptionType.Free, subscriptionId1),
            new(subscriptionType: SubscriptionType.Starter, subscriptionId2),
            new(subscriptionType: SubscriptionType.Pro, Guid.CreateVersion7()),
        };

        dbContext.Clubs.AddRange(clubs);
        dbContext.Subscriptions.AddRange(subscriptions);
        await dbContext.SaveChangesAsync();
    //}
    
}

app.MapControllers();
await app.RunAsync();
