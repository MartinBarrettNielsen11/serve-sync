using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Persistence;

internal sealed class UserDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        return 0;
    }
}