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
	/*
	[Fact]
	public async Task Add_Instructor()
	{
	} */
}
