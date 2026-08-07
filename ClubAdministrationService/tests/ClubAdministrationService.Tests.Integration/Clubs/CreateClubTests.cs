using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Tests.Integration.Extensions;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

public sealed class CreateClubTests(ApiTestFixture fixture) : BaseApiTest(fixture), IClassFixture<ApiTestFixture>
{
	[Fact]
	public async Task Success()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		InitialDbContext.Subscriptions.Add(sub);
		await InitialDbContext.SaveChangesAsync();

		CreateClubRequest request = new("Test Club");

		// Act
		HttpResponseMessage response = await Client.PostAsJsonAsync($"api/v1/subscriptions/{sub.Id}/clubs",
																	request);

		// Assert
		Assert.Equal(HttpStatusCode.Created, response.StatusCode);

		await using ClubDbContext assertionContext = Fixture.CreateDbContext();
		Subscription? updatedSubscription =
			await assertionContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == sub.Id);

		Assert.NotNull(updatedSubscription);
		Assert.NotEmpty(updatedSubscription.ClubIds);

		GetFakeLogCollector()
			.ShouldHaveInformationLog("Club created: {Name}",
									("Name", "Test Club"));
	}
}
