using System.Net;
using ClubAdministrationService.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

// note: you need to add a some abstraction that includes setting up a database, a fake logger, and some InitialDbContext, whcih can be worked with in the arrange step.
public class ListClubsTests(WebApplicationFactory<IApiMarker> appFactory)
	: IClassFixture<WebApplicationFactory<IApiMarker>>
{
	private readonly HttpClient _httpClient = appFactory.CreateClient();

	[Fact]
	public async Task ListClubs_happy_path()
	{
		// Arrange
		Guid subscriptionId = Guid.Parse("019e9d1e-b2fa-7ada-baad-97bb06ac3889");

		// Act

		Uri requestUri = new($"/subscriptions/{subscriptionId}/clubs",
							UriKind.Relative);

		HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		var result = await response.Content.ReadAsStringAsync();

		Assert.NotEqual("", result);
	}


	[Fact]
	public async Task ListClubs_unhappy_path()
	{
		// Arrange
		Guid subscriptionId = Guid.Parse("11111111-1111-1111-1111-111111111111");

		// Act
		Uri requestUri = new($"/subscriptions/{subscriptionId}/clubs",
							UriKind.Relative);

		HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

		var result = await response.Content.ReadAsStringAsync();

		Assert.NotEqual("", result);
	}
}
