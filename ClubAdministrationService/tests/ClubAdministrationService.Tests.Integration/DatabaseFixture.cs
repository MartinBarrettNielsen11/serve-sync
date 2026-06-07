using ClubAdministrationService.Persistence;

namespace ClubAdministrationService.Tests.Integration;

internal sealed class DatabaseFixture
{
    internal required string ConnectionString { get; } = null!;

    internal ClubDbContext CreateDbContext()
    {
        ...
    }
}