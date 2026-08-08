namespace UserAdministrationService.Tests.Integration;

public abstract class BaseApiTest : BaseIntegrationTest
{
	protected BaseApiTest(ApiTestFixture fixture, int apiVersion = 1)
		: base(fixture)
	{
		Client = fixture.CreateClient();
		Client.BaseAddress = new Uri(Client.BaseAddress!, "api/v{apiVersion}/");
	}

	protected HttpClient Client { get; }
}
