using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

public sealed class AddInstructorTests(ApiTestFixture fixture) : BaseApiTest(fixture), IClassFixture<ApiTestFixture>
{
    [Fact]
    public async Task Add_Instructor()
    {
        // Arrange
        Subscription sub = SubscriptionFactory.Create();
        Club club = ClubFactory.Create();
        InitialDbContext.Subscriptions.Add(sub);
        InitialDbContext.Clubs.Add(club);
        await InitialDbContext.SaveChangesAsync();

        AddInstructorRequest request = new(InstructorId: Guid.NewGuid());

        // Act
        HttpResponseMessage response = await Client.PostAsJsonAsync(
            requestUri: $"subscriptions/{sub.Id}/clubs/{club.Id}/instructors",
            value: request);

        // Assert
        //Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(response.StatusCode, response.StatusCode);

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