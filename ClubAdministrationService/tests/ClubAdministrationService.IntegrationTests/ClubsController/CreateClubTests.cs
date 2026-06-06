using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ClubAdministrationService.IntegrationTests.ClubsController;

// note: you need to add a some abstraction that includes setting up a database, a fake logger, and some InitialDbContext, whcih can be worked with in the arrange step.
public class CreateClubTests : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public CreateClubTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task Create_Club()
    {
        // Arrange
        Guid subscriptionId = Guid.NewGuid();

        //Subscription yo = SubscriptionFactory.Create(subscriptionType: SubscriptionType.Free);

        CreateClubRequest request = new(Name: "Test Club");

        // Act
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            requestUri: $"/subscriptions/{subscriptionId}/clubs",
            value: request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}