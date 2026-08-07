using ClubAdministrationService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;

namespace ClubAdministrationService.Tests.Integration;

public abstract class BaseIntegrationTest
{
	internal readonly ClubDbContext InitialDbContext;
	private readonly IServiceScope _scope;

	protected BaseIntegrationTest(ApiTestFixture fixture)
	{
		Fixture = fixture;
		_scope = fixture.Services.CreateScope();
		InitialDbContext = _scope.ServiceProvider.GetRequiredService<ClubDbContext>();

		// remove this - and apply in composition root at some later day
		if (InitialDbContext.Database.GetPendingMigrations().Any())
		{
			InitialDbContext.Database.Migrate();
		}

		ResetLoggingStorage();
	}

	protected ApiTestFixture Fixture { get; }

	internal ClubDbContext GetDbContext()
	{
		return _scope.ServiceProvider.GetRequiredService<ClubDbContext>();
	}

	protected FakeLogCollector GetFakeLogCollector()
	{
		return _scope.ServiceProvider.GetRequiredService<FakeLogCollector>();
	}

	private void ResetLoggingStorage()
	{
		_scope.ServiceProvider.GetFakeLogCollector().Clear();
	}
}
