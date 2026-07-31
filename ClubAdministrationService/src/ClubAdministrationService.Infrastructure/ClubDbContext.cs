using System.Reflection;
using ClubAdministrationService.Domain.AdminAggregate;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using ClubAdministrationService.Infrastructure.IntegrationEvents;
using ClubAdministrationService.Infrastructure.Middleware;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ClubAdministrationService.Infrastructure;

internal sealed class ClubDbContext(
    DbContextOptions<ClubDbContext> options,
	IHttpContextAccessor httpContextAccessor,
	IPublisher publisher)
	: DbContext(options)
{
	public DbSet<Admin> Admins { get; set; } = null!;
	public DbSet<Subscription> Subscriptions { get; set; } = null!;
	public DbSet<Club> Clubs { get; set; } = null!;
    public DbSet<OutboxIntegrationEvent> OutboxIntegrationEvents { get; set; } = null!;

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

        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
            return await base.SaveChangesAsync(cancellationToken);
        }

		await PublishDomainEventsAsync(domainEvents);
		return await base.SaveChangesAsync(cancellationToken);
	}

    private bool IsUserWaitingOnline() => httpContextAccessor.HttpContext is not null;

    private async Task PublishDomainEventsAsync(List<IDomainEvent> domainEvents)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        Queue<IDomainEvent> domainEventsQueue;
        IDictionary<object, object?> items = httpContextAccessor.HttpContext!.Items;

        if (items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
            value is Queue<IDomainEvent> existingDomainEvents)
        {
            domainEventsQueue = existingDomainEvents;
        }
        else
        {
            domainEventsQueue = new Queue<IDomainEvent>();
        }

        domainEvents.ForEach(domainEventsQueue.Enqueue);
        httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
    }
}
