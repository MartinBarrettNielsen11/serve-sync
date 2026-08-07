namespace UserAdministrationService.Tests.Integration;

public abstract class BaseApiTest(ApiTestFixture fixture) : BaseIntegrationTest(fixture)
{
	protected HttpClient Client { get; } = fixture.CreateClient();
}
