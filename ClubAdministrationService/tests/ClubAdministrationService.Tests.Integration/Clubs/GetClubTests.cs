using System.Net;
using System.Net.Http.Json;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;


public sealed class GetClubTests(ApiTestFixture fixture) :
	BaseApiTest(fixture, apiVersion: 1), IClassFixture<ApiTestFixture>
{
	[Fact]
	public async Task Success()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		Club club = ClubFactory.Create(subscriptionId: sub.Id);
		InitialDbContext.Subscriptions.Add(sub);
		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		// Act
		HttpResponseMessage response = await Client.GetAsync(
			requestUri: new Uri($"subscriptions/{sub.Id}/clubs/{club.Id}",
								UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
		ClubResponse? clubResponse = await response.Content.ReadFromJsonAsync<ClubResponse>();
		Assert.NotNull(clubResponse);
		Assert.Equal(expected: club.Id, clubResponse.Id);
		Assert.Equal(expected: club.Name, actual: clubResponse.Name);
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
			requestUri: new Uri($"subscriptions/{invalidSubscriptionId}/clubs/{club.Id}",
								UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Subscription not found", problemDetails.Detail!);
	}

	[Fact]
	public async Task When_ClubIdIsInvalid_Then_ErrorIsReturned()
	{
		// Arrange
		Subscription sub = SubscriptionFactory.Create(SubscriptionType.Pro);
		Club club = ClubFactory.Create(subscriptionId: sub.Id);
		InitialDbContext.Subscriptions.Add(sub);
		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		// Act
		Guid invalidClubId = Guid.NewGuid();
		HttpResponseMessage response = await Client.GetAsync(
			requestUri: new Uri($"subscriptions/{sub.Id}/clubs/{invalidClubId}",
								UriKind.Relative));

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Club not found", problemDetails.Detail!);
	}
}
