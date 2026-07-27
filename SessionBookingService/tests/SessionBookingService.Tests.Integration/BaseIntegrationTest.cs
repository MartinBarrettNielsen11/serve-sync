using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Testing;
using SessionBookingService.Infrastructure;

namespace SessionBookingService.Tests.Integration;

public abstract class BaseIntegrationTest
{
	private readonly IServiceScope _scope;
	internal readonly SessionBookingDbContext InitialDbContext;

	protected BaseIntegrationTest(ApiTestFixture fixture)
	{
		Fixture = fixture;
		_scope = fixture.Services.CreateScope();
		InitialDbContext = _scope.ServiceProvider.GetRequiredService<SessionBookingDbContext>();

		// remove this - and apply in composition root at some later day
		if (InitialDbContext.Database.GetPendingMigrations().Any())
        {
            InitialDbContext.Database.Migrate();
        }

        ResetLoggingStorage();
	}

	protected ApiTestFixture Fixture { get; }

	internal SessionBookingDbContext GetDbContext()
	{
		return _scope.ServiceProvider.GetRequiredService<SessionBookingDbContext>();
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
