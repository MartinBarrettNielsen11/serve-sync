using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SessionBookingService.Infrastructure;
using SessionBookingService.WebApi;
using Testcontainers.PostgreSql;
using Xunit;

namespace SessionBookingService.Tests.Integration;

public sealed class ApiTestFixture : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
	private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:17")
		.WithEnvironment("POSTGRES_USER", "postgres")
		.WithEnvironment("POSTGRES_PASSWORD", "postgres")
		.WithEnvironment("POSTGRES_DB", "postgres")
		.WithPortBinding(5432, assignRandomHostPort: true)
		//.WithWaitStrategy(Wait.ForUnixContainer())
		.Build();

	private string ConnectionString => _dbContainer.GetConnectionString();

	public async Task InitializeAsync() => await _dbContainer.StartAsync();

	public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Testing");

		builder.ConfigureLogging(logging => { logging.ClearProviders(); });

		builder.ConfigureLogging(b => { b.AddFakeLogging(); });

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll<IHostedService>();

			// replace db registration
			services.RemoveAll<DbContextOptions<SessionBookingDbContext>>();
			services.RemoveAll<SessionBookingDbContext>();

			services.AddDbContext<SessionBookingDbContext>(options => { options.UseNpgsql(ConnectionString); });


			// replace clock

			// replace external integrations
		});
	}

	internal SessionBookingDbContext CreateDbContext()
	{
		DbContextOptions<SessionBookingDbContext> options = new DbContextOptionsBuilder<SessionBookingDbContext>()
			.UseNpgsql(ConnectionString)
			.Options;

		return new SessionBookingDbContext(options: options, httpContextAccessor: new HttpContextAccessor(), publisher: null!);
	}
}
