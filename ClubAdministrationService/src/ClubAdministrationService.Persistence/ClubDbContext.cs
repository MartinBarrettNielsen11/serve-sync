using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using SharedKernel;

namespace ClubAdministrationService.Persistence;

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
    
    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent, httpContextAccessor.HttpContext!.RequestAborted);
        }
    }
}