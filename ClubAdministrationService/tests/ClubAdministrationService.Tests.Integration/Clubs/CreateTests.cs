using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
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
        
        await using var assertionContext = GetDbContext();
        Club? yo = await assertionContext.Clubs.FirstOrDefaultAsync(c => c.SubscriptionId == sub.Id);
        
        Assert.NotNull(yo);
        Assert.Equal("Test Club", yo.Name);

        Club? club = await InitialDbContext.Clubs.FirstOrDefaultAsync(c => c.SubscriptionId == sub.Id);
        
        Assert.NotNull(club);
        Assert.Equal("Test Club", club.Name);

        IReadOnlyList<FakeLogRecord> logs = GetFakeLogCollector().GetSnapshot();

        IReadOnlyList<KeyValuePair<string, string?>>? record = logs
            .Where(l => l.Level == LogLevel.Information)
            .Where(l => l.StructuredState is not null && 
                        l.StructuredState.Any(kvp => string.Equals(kvp.Key, "Name", StringComparison.OrdinalIgnoreCase) &&
                                                     string.Equals(kvp.Value, "Test Club", StringComparison.OrdinalIgnoreCase)))
            .Select(l => l.StructuredState)
            .FirstOrDefault();
        
        Assert.NotNull(record);
        
        Assert.Contains(record,
            kvp => string.Equals(kvp.Key, "{OriginalFormat}", StringComparison.OrdinalIgnoreCase) &&
string.Equals(kvp.Value, "Club was not created yet :) but here is some text: {Name}", StringComparison.OrdinalIgnoreCase));
        
        Assert.NotNull(record);
    }
}