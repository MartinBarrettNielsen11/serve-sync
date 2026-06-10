using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using ClubAdministrationService.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.ClubsController;

// note: you need to add a some abstraction that includes setting up a database, a fake logger, and some InitialDbContext, whcih can be worked with in the arrange step.
public sealed class CreateClubTests(ApiTestFixture fixture) : BaseApiTest(fixture), IClassFixture<ApiTestFixture>
{
    [Fact]
    public async Task Create_Club()
    {
        // Arrange
        Subscription sub = SubscriptionFactory.Create();
        InitialDbContext.Subscriptions.Add(sub);
        await InitialDbContext.SaveChangesAsync();
        
        CreateClubRequest request = new(Name: "Test Club");

        // Act
        HttpResponseMessage response = await Client.PostAsJsonAsync(
            requestUri: $"subscriptions/{sub.Id}/clubs", 
            value: request);

        // Assert
        //Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(response.StatusCode, response.StatusCode);

        IReadOnlyList<FakeLogRecord> logs = GetFakeLogCollector().GetSnapshot();
        IEnumerable<IReadOnlyList<KeyValuePair<string, string?>>?> relevantLogs = logs.Where(l => l.Level == LogLevel.Information).Select(l => l.StructuredState);

        IReadOnlyList<KeyValuePair<string, string?>>? record = logs.Where(log =>
                log.StructuredState is not null &&
                log.StructuredState.Any(kvp => string.Equals(kvp.Key, "Name", StringComparison.OrdinalIgnoreCase) &&
                                               string.Equals(kvp.Value, "Test Club",
                                                   StringComparison.OrdinalIgnoreCase)))
            .Select(l => l.StructuredState)
            .FirstOrDefault();
        
        Assert.NotNull(record);
        
        Assert.Contains(record,
            kvp => string.Equals(kvp.Key, "{OriginalFormat}", StringComparison.OrdinalIgnoreCase) &&
string.Equals(kvp.Value, "Club was not created yet :) but here is some text: {Name}", StringComparison.OrdinalIgnoreCase));
        
        Assert.NotNull(record);
        
        Assert.NotEqual(relevantLogs, []);
    }
}