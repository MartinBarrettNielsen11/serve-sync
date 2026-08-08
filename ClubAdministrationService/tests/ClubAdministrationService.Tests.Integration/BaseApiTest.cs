using System.Globalization;

namespace ClubAdministrationService.Tests.Integration;

public abstract class BaseApiTest : BaseIntegrationTest
{
	protected BaseApiTest(ApiTestFixture fixture, int apiVersion = 1)
		: base(fixture)
	{
		Client = fixture.CreateClient();
		Client.BaseAddress = new Uri(baseUri: Client.BaseAddress!,
									 relativeUri: $"api/v{apiVersion.ToString(CultureInfo.InvariantCulture)}/");
	}

	protected HttpClient Client { get; }
}
