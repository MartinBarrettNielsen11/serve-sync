using System.Reflection;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.PlayerAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SessionBookingService.Infrastructure.Middlewares;
using SharedKernel;

namespace SessionBookingService.Infrastructure;

internal class SessionBookingDbContext(
	DbContextOptions options,
	IHttpContextAccessor httpContextAccessor,
	IPublisher publisher) : DbContext(options)
{
	public DbSet<Court> Courts { get; set; } = null!;
	public DbSet<Session> Sessions { get; set; } = null!;
	public DbSet<Instructor> Instructors { get; set; } = null!;
	public DbSet<Player> Players { get; set; } = null!;

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

		await PublishDomainEvents(domainEvents);
		return await base.SaveChangesAsync(cancellationToken);
	}

	private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
	{
		foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

	private bool IsUserWaitingOnline()
	{
		return httpContextAccessor.HttpContext is not null;
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
