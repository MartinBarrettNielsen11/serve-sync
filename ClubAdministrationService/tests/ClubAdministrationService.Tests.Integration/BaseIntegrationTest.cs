using ClubAdministrationService.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;

namespace ClubAdministrationService.Tests.Integration;

public abstract class BaseIntegrationTest
{
    //protected readonly ClubAdministrationDbContext InitialDbContext;

    protected readonly ApiTestFixture _fixture;
    protected readonly IServiceScope _scope;

    protected BaseIntegrationTest(ApiTestFixture fixture, IServiceScope scope)
    {
        _scope = scope;
        _fixture = fixture;
        ResetLoggingStorage();

        //InitialDbContext = fixture.CreateDbContext();

        //ResetDatabase();
    }
    
    
    protected ClubDbContext GetDbContext()
    {
        return _factory.CreateDbContext();
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