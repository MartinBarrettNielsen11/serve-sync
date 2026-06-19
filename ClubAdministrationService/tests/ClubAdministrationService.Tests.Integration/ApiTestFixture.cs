using ClubAdministrationService.Persistence;
using ClubAdministrationService.WebApi;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
            .WithEnvironment(name: "POSTGRES_USER", value: "postgres")
            .WithEnvironment(name: "POSTGRES_PASSWORD", value: "postgres")
            .WithEnvironment(name: "POSTGRES_DB", value: "postgres")
            .WithPortBinding(port: 5432, assignRandomHostPort: true)
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
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IHostedService>();
            
            // replace db registration
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

    internal ClubDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ClubDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        return new ClubDbContext(
            options,
            new HttpContextAccessor(),
            null!);
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