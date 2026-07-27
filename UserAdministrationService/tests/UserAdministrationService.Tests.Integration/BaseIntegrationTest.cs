using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;
using UserAdministrationService.Infrastructure;

namespace UserAdministrationService.Tests.Integration;

public abstract class BaseIntegrationTest
{
	private readonly IServiceScope _scope;
	internal readonly UserDbContext InitialDbContext;

	protected BaseIntegrationTest(ApiTestFixture fixture)
	{
		Fixture = fixture;
		_scope = fixture.Services.CreateScope();
		InitialDbContext = _scope.ServiceProvider.GetRequiredService<UserDbContext>();

		// remove this - and apply in composition root at some later day
		if (InitialDbContext.Database.GetPendingMigrations().Any())
        {
            InitialDbContext.Database.Migrate();
        }

        ResetLoggingStorage();
	}

	protected ApiTestFixture Fixture { get; }

	internal UserDbContext GetDbContext()
	{
		return _scope.ServiceProvider.GetRequiredService<UserDbContext>();
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
