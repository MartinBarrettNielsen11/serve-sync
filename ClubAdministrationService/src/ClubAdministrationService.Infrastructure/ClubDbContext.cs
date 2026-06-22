using System.Reflection;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ClubAdministrationService.Infrastructure;

internal sealed class ClubDbContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor,
    IPublisher publisher)
    : DbContext(options)
{
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<RootAggregate>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }
    
#pragma warning disable VSTHRD200
    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
#pragma warning restore VSTHRD200
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, httpContextAccessor.HttpContext!.RequestAborted);
        }
    }
}