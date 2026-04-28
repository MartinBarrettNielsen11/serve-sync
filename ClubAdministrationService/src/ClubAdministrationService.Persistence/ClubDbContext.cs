using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using MediatR;
using SharedKernel;

namespace ClubAdministrationService.Persistence;

internal sealed class ClubDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPublisher _publisher;

    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Club> Clubs { get; set; } = null!;

    public ClubDbContext(DbContextOptions options, 
                         IHttpContextAccessor httpContextAccessor,
                         IPublisher publisher) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    
    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}