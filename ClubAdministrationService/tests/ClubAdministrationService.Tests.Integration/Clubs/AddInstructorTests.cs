using System.Net;
using System.Net.Http.Json;
using Asp.Versioning;
using ClubAdministrationService.Contracts.Clubs;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.Tests.Integration.Extensions;
using ClubAdministrationService.Tests.Unit.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Xunit;

namespace ClubAdministrationService.Tests.Integration.Clubs;

public sealed class AddInstructorTests(ApiTestFixture fixture) :
	BaseApiTest(fixture, apiVersion: 1), IClassFixture<ApiTestFixture>
{
	[Fact]
	public async Task Success()
	{
		// Arrange
		Guid subscriptionId = Guid.CreateVersion7();
		Club club = ClubFactory.Create(name: "Club1",
									   subscriptionId: subscriptionId,
									   id: Guid.CreateVersion7());

		Subscription sub = SubscriptionFactory.CreateWithClub(club: club,
															  subscriptionType: SubscriptionType.Pro,
															  id: subscriptionId);

		InitialDbContext.Subscriptions.Add(sub);
		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		Guid instructorId = Guid.CreateVersion7();
		AddInstructorRequest request = new(instructorId);

		// Act
		HttpResponseMessage response = await Client.PostAsJsonAsync(
			requestUri: $"subscriptions/{sub.Id}/clubs/{club.Id}/instructors",
			value: request);

		// Assert
		Assert.Equal(expected: HttpStatusCode.OK, actual: response.StatusCode);
		Guid? clubResponse = await response.Content.ReadFromJsonAsync<Guid>();

		Assert.NotNull(clubResponse);

		await using ClubDbContext assertionContext = Fixture.CreateDbContext();
		Club updatedClub = await assertionContext.Clubs.FirstAsync(c => c.Id == clubResponse.Value);
		var hasInstructor = updatedClub.HasInstructor(instructorId);
		Assert.True(hasInstructor);
	}

	[Fact]
	public async Task When_SubscriptionDoesNotExist_Then_ErrorTypeSubscriptionNotFoundIsReturned()
	{
		// Arrange
		Guid subscriptionId = Guid.CreateVersion7();
		Club club = ClubFactory.Create(name: "Club1",
										subscriptionId: subscriptionId,
										id: Guid.CreateVersion7());

		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		Guid instructorId = Guid.CreateVersion7();
		AddInstructorRequest request = new(instructorId);

		// Act
		HttpResponseMessage response = await Client.PostAsJsonAsync(
			requestUri: $"subscriptions/{Guid.CreateVersion7()}/clubs/{club.Id}/instructors",
			value: request);

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Subscription not found", actual: problemDetails.Detail!);

		await using ClubDbContext assertionContext = Fixture.CreateDbContext();
		Club clubFromDb = await assertionContext.Clubs.FirstAsync(c => c.Id == club.Id);
		var hasInstructor = clubFromDb.HasInstructor(instructorId);
		Assert.False(hasInstructor);
	}

	[Fact]
	public async Task When_ClubDoesNotHaveSubscription_Then_ErrorTypeClubNotFoundIsReturned()
	{
		// Arrange
		Guid subscriptionId = Guid.CreateVersion7();
		Club club = ClubFactory.Create(name: "Club1",
										subscriptionId: subscriptionId,
										id: Guid.CreateVersion7());

		Subscription sub = SubscriptionFactory.Create(subscriptionType: SubscriptionType.Pro,
													  id: subscriptionId);

		InitialDbContext.Subscriptions.Add(sub);
		InitialDbContext.Clubs.Add(club);
		await InitialDbContext.SaveChangesAsync();

		Guid instructorId = Guid.CreateVersion7();
		AddInstructorRequest request = new(instructorId);

		// Act
		HttpResponseMessage response = await Client.PostAsJsonAsync(
			requestUri: $"subscriptions/{sub.Id}/clubs/{club.Id}/instructors",
			value: request);

		// Assert
		Assert.Equal(expected: HttpStatusCode.NotFound, actual: response.StatusCode);
		ProblemDetails? problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
		Assert.NotNull(problemDetails);
		Assert.Equal(expected: "Club not found", actual: problemDetails.Detail!);

		await using ClubDbContext assertionContext = Fixture.CreateDbContext();
		Club clubFromDb = await assertionContext.Clubs.FirstAsync(c => c.Id == club.Id);
		var hasInstructor = clubFromDb.HasInstructor(instructorId);
		Assert.False(hasInstructor);
	}

}
