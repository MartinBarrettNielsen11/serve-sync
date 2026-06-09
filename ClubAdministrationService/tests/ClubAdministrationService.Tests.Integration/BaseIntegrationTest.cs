using ClubAdministrationService.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;

namespace ClubAdministrationService.Tests.Integration;

public abstract class BaseIntegrationTest
{
    //protected readonly ClubAdministrationDbContext InitialDbContext;

    protected ApiTestFixture Fixture { get; }
    private readonly IServiceScope _scope;

    protected BaseIntegrationTest(ApiTestFixture fixture)
    {
        Fixture = fixture;
        _scope = fixture.Services.CreateScope();

        ResetLoggingStorage();

        //InitialDbContext = fixture.CreateDbContext();

        //ResetDatabase();
    }
    
    internal ClubDbContext GetDbContext()
    {
        return _scope.ServiceProvider.GetRequiredService<ClubDbContext>();
    }
    
    protected FakeLogCollector GetFakeLogCollector()
    {
        return _scope.ServiceProvider.GetRequiredService<FakeLogCollector>();
    }
    
    protected void ResetLoggingStorage()
    {
        _scope.ServiceProvider.GetFakeLogCollector().Clear();
    }
}