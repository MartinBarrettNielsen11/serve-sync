namespace ClubAdministrationService.Tests.Integration;

public abstract class BaseApiTest : BaseIntegrationTest
{
	protected HttpClient Client { get; }

	protected BaseApiTest(ApiTestFixture fixture) : base(fixture)
	{
		Client = fixture.CreateClient();
	}
}
