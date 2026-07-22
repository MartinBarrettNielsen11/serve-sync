using UserAdministrationService.Tests.Integration;

namespace UserAdministrationService.Tests.Integration;

public abstract class BaseApiTest : BaseIntegrationTest
{
	protected HttpClient Client { get; }

	protected BaseApiTest(ApiTestFixture fixture) : base(fixture)
	{
		Client = fixture.CreateClient();
	}
}
