namespace ClubAdministrationService.Tests.Integration;

internal abstract class BaseApiTest : BaseIntegrationTest
{
    // ReSharper disable once NotAccessedField.Global
#pragma warning disable CA1051
    protected readonly HttpClient Client;
#pragma warning restore CA1051

    protected BaseApiTest(ApiTestFixture fixture) : base(fixture)
    {
        Client = fixture.CreateClient();
    }
}