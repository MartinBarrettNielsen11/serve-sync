using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.ClubsController;

// note: you need to add a some abstraction that includes setting up a database, a fake logger, and some InitialDbContext, whcih can be worked with in the arrange step.
public sealed class CreateClubTests : BaseApiTest, IClassFixture<ApiTestFixture>
{
    public CreateClubTests(ApiTestFixture fixture) : base(fixture) { }
    
    [Fact]
    public async Task Create_Club()
    {
        // Arrange
        Guid subscriptionId = Guid.NewGuid();

        //Subscription yo = SubscriptionFactory.Create(subscriptionType: SubscriptionType.Free);

        CreateClubRequest request = new(Name: "Test Club");

        // Act
        HttpResponseMessage response = await Client.PostAsJsonAsync(
            requestUri: $"api/subscriptions/{subscriptionId}/clubs", 
            value: request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        IReadOnlyList<FakeLogRecord> logs = GetFakeLogCollector().GetSnapshot();
        Assert.NotEqual(logs, []);
    }
}