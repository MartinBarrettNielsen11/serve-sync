using System.Reflection;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ClubAdministrationService.Persistence;

internal sealed class ClubDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;

    internal ClubDbContext(DbContextOptions options, 
                           IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}