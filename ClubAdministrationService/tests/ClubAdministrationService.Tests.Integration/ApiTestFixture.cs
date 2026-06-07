using ClubAdministrationService.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAdministrationService.Tests.Integration;

public sealed class ApiTestFixture : WebApplicationFactory<IApiMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            ServiceDescriptor serviceDescriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions));
            services.Remove(serviceDescriptor);
            
            // replace db registration

            // replace clock

            // replace external integrations

            // fake logger
            
            
            
            
        });
    }
}