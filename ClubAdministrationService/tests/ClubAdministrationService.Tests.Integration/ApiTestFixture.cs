using ClubAdministrationService.Persistence;
using ClubAdministrationService.WebApi;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

namespace ClubAdministrationService.Tests.Integration;

public sealed class ApiTestFixture : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    internal required string ConnectionString { get; init; } = null!;

    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder(image: "postgres:17")
            .WithEnvironment("POSTGRES_USER", "course")
            .WithEnvironment("POSTGRES_PASSWORD", "changeme")
            .WithEnvironment("POSTGRES_DB", "mydb")
            .WithPortBinding(5555, 5432)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilContainerIsHealthy())
            .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // replace db registration
            ServiceDescriptor serviceDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions));
            services.Remove(serviceDescriptor);
            
            services.AddDbContext<ClubDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });


            // replace clock

            // replace external integrations
            
            
            
        });
    }
    
    internal ClubDbContext CreateDbContext()
    {
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}