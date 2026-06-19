using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Integration.Extensions;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

public sealed class CreateTests(ApiTestFixture fixture) : BaseApiTest(fixture), IClassFixture<ApiTestFixture>
{
    [Fact]
    public async Task Create_Club()
    {
        // Arrange
        Subscription sub = SubscriptionFactory.Create(subscriptionType: SubscriptionType.Pro);
        InitialDbContext.Subscriptions.Add(sub);
        await InitialDbContext.SaveChangesAsync();
        
        CreateClubRequest request = new(Name: "Test Club");

        // Act
        HttpResponseMessage response = await Client.PostAsJsonAsync(
            requestUri: $"subscriptions/{sub.Id}/clubs", 
            value: request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        await using var assertionContext = Fixture.CreateDbContext();
        Subscription? updatedSubscription = await assertionContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == sub.Id);
        
        Assert.NotNull(updatedSubscription);
        Assert.NotEmpty(updatedSubscription.ClubIds);
        
        GetFakeLogCollector().ShouldHaveInformationLog(
            "Club was not created yet :) but here is some text: {Name}",
            ("Name", "Test Club"));
    }
}