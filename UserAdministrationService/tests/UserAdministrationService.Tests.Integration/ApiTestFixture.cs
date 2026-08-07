using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using UserAdministrationService.Infrastructure;
using UserAdministrationService.WebApi;
using Xunit;

namespace UserAdministrationService.Tests.Integration;

public sealed class ApiTestFixture : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
	private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:17")
														.WithEnvironment("POSTGRES_USER", "postgres")
														.WithEnvironment("POSTGRES_PASSWORD", "postgres")
														.WithEnvironment("POSTGRES_DB", "postgres")
														.WithPortBinding(5432, true)
														//.WithWaitStrategy(Wait.ForUnixContainer())
														.Build();

	private string ConnectionString => _dbContainer.GetConnectionString();

	public async Task InitializeAsync()
	{
		await _dbContainer.StartAsync();
	}

	public new async Task DisposeAsync()
	{
		await _dbContainer.DisposeAsync();
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Testing");

		builder.ConfigureLogging(logging => { logging.ClearProviders(); });

		builder.ConfigureLogging(b => { b.AddFakeLogging(); });

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll<IHostedService>();

			// replace db registration
			services.RemoveAll<DbContextOptions<UserDbContext>>();
			services.RemoveAll<UserDbContext>();

			services.AddDbContext<UserDbContext>(opts => opts.UseNpgsql(ConnectionString));


			// replace clock

			// replace external integrations
		});
	}

	internal UserDbContext CreateDbContext()
	{
		DbContextOptions<UserDbContext> options = new DbContextOptionsBuilder<UserDbContext>()
												.UseNpgsql(ConnectionString)
												.Options;

		return new UserDbContext(options, new HttpContextAccessor());
	}
}
