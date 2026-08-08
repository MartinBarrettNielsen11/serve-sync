using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Tests.Integration.Extensions;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

public sealed class CreateClubTests(ApiTestFixture fixture) :
	BaseApiTest(fixture, apiVersion: 1), IClassFixture<ApiTestFixture>
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
		HttpResponseMessage response = await Client.PostAsJsonAsync(
			requestUri: $"subscriptions/{sub.Id}/clubs",
			value: request);

		// Assert
		Assert.Equal(expected: HttpStatusCode.Created, actual: response.StatusCode);
		ClubResponse? clubResponse = await response.Content.ReadFromJsonAsync<ClubResponse>();

		Assert.NotNull(clubResponse);
		Assert.Equal(expected: clubResponse.Name, actual: request.Name);

		await using ClubDbContext assertionContext = Fixture.CreateDbContext();
		Subscription? updatedSubscription = await assertionContext.Subscriptions
			.FirstOrDefaultAsync(s => s.Id == sub.Id);

		Assert.NotNull(updatedSubscription);
		Assert.NotEmpty(updatedSubscription.ClubIds);

		GetFakeLogCollector().ShouldHaveInformationLog(messageTemplate: "Club created: {Name}",
													  (Key: "Name", Value: "Test Club"));
	}

	[Fact]
	public async Task When_SubscriptionDoesNotExist_Then_ErrorIsReturned()
	{
		// Arrange
		Guid subscriptionId = Guid.NewGuid();
		CreateClubRequest request = new("Test Club");

		// Act
		HttpResponseMessage response = await Client.PostAsJsonAsync(
			requestUri: $"subscriptions/{subscriptionId}/clubs",
			value: request);

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Subscription not found", actual: problemDetails.Detail!);
	}
}
