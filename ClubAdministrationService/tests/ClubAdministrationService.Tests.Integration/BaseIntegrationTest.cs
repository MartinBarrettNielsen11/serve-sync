namespace ClubAdministrationService.Tests.Integration;

internal abstract class BaseIntegrationTest
{
    //protected readonly ClubAdministrationDbContext InitialDbContext;

    private readonly ApiTestFixture _fixture;

    protected BaseIntegrationTest(ApiTestFixture fixture)
    {
        _fixture = fixture;

        //InitialDbContext = factory.CreateDbContext();

        //ResetDatabase();
    }
    
    /*
    protected ClubAdministrationDbContext GetDbContext()
    {
        return _factory.CreateDbContext();
    }
    */
}