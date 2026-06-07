using ClubAdministrationService.Persistence;
using Xunit;

namespace ClubAdministrationService.Tests.Integration;

internal sealed class DatabaseFixture : IAsyncLifetime
{
    internal required string ConnectionString { get; } = null!;

    internal ClubDbContext CreateDbContext()
    {
        ...
    }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}