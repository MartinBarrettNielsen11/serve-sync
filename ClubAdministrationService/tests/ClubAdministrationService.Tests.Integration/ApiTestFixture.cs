using ClubAdministrationService.Persistence;
using ClubAdministrationService.WebApi;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Xunit;

namespace ClubAdministrationService.Tests.Integration;

public sealed class ApiTestFixture : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    public string ConnectionString => _dbContainer.GetConnectionString();


    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder(image: "postgres:17")
            .WithEnvironment(name: "POSTGRES_USER", value: "course")
            .WithEnvironment(name: "POSTGRES_PASSWORD", value: "changeme")
            .WithEnvironment(name: "POSTGRES_DB", value: "mydb")
            .WithPortBinding(hostPort: 5555, containerPort: 5432)
        
            //.WithWaitStrategy(Wait.ForUnixContainer())
            .Build();
        
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        });
        
        builder.ConfigureLogging(b =>
        {
            b.AddFakeLogging();
        });
        
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IHostedService>();
            
            // replace db registration
            ServiceDescriptor serviceDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions));
            services.Remove(serviceDescriptor);
            
            services.RemoveAll<DbContextOptions<ClubDbContext>>();
            services.RemoveAll<ClubDbContext>();

            services.AddDbContext<ClubDbContext>(options =>
            {
                options.UseNpgsql(ConnectionString);
            });


            // replace clock

            // replace external integrations
            
            
            
        });
    }
    
    /*
    internal ClubDbContext CreateDbContext()
    {
    } */

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}