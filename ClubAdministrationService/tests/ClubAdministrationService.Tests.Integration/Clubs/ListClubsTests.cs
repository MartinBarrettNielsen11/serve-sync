using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;


public sealed class ListClubsTests(ApiTestFixture fixture) :
	BaseApiTest(fixture, apiVersion: 1), IClassFixture<ApiTestFixture>
{
	[Fact]
	public async Task Success()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		Club club1 = ClubFactory.Create(name: "Club1", subscriptionId: sub.Id, id: Guid.CreateVersion7());
		Club club2 = ClubFactory.Create(name: "Club2", subscriptionId: sub.Id, id: Guid.CreateVersion7());
		InitialDbContext.Subscriptions.Add(sub);
		await InitialDbContext.Clubs.AddRangeAsync(club1, club2);
		await InitialDbContext.SaveChangesAsync();

		// Act
		HttpResponseMessage response = await Client.GetAsync(
			requestUri: new Uri(uriString: $"subscriptions/{sub.Id}/clubs", uriKind: UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
		List<ClubResponse>? clubResponse = await response.Content.ReadFromJsonAsync<List<ClubResponse>>();
		Assert.NotNull(clubResponse);
		var expected = new[]
		{
			new
			{
				club1.Id,
				club1.Name
			},
			new
			{
				club2.Id,
				club2.Name
			}
		};

		Assert.Equivalent(
			expected: expected,
			actual: clubResponse,
			strict: true);
	}

	[Fact]
	public async Task When_NoClubsExistForSubscription_Then_EmptyResponseIsReturned()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		InitialDbContext.Subscriptions.Add(sub);
		await InitialDbContext.SaveChangesAsync();

		// Act
		HttpResponseMessage response = await Client.GetAsync(
			requestUri: new Uri(uriString: $"subscriptions/{sub.Id}/clubs", uriKind: UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
		List<ClubResponse>? clubResponse = await response.Content.ReadFromJsonAsync<List<ClubResponse>>();
		Assert.NotNull(clubResponse);
		Assert.Empty(clubResponse);
	}

	[Fact]
	public async Task When_SubscriptionIdIsInvalid_Then_ErrorIsReturned()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		Club club = ClubFactory.Create(subscriptionId: sub.Id);
		InitialDbContext.Subscriptions.Add(sub);
		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		// Act
		Guid invalidSubscriptionId = Guid.NewGuid();
		HttpResponseMessage response = await Client.GetAsync(
			requestUri: new Uri($"subscriptions/{invalidSubscriptionId}/clubs",
								UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Subscription not found", problemDetails.Detail!);
	}
}
