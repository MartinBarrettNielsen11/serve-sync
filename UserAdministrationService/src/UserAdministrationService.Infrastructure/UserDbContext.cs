using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using UserAdministrationService.Domain.UserAggregate;
using UserAdministrationService.Infrastructure.IntegrationEvents;

namespace UserAdministrationService.Infrastructure;

internal sealed class UserDbContext(DbContextOptions<UserDbContext> options, IHttpContextAccessor httpContextAccessor)
	: DbContext(options)
{
	public DbSet<User> Users { get; set; } = null!;
	public DbSet<OutboxIntegrationEvent> OutboxIntegrationEvents { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		if (httpContextAccessor.HttpContext is null) return await base.SaveChangesAsync(cancellationToken);

		List<IDomainEvent> domainEvents = ChangeTracker.Entries<RootAggregate>()
			.Select(entry => entry.Entity.PopDomainEvents())
			.SelectMany(x => x)
			.ToList();

		var result = await base.SaveChangesAsync(cancellationToken);

		Queue<IDomainEvent> domainEventsQueue;
		IDictionary<object, object?> items = httpContextAccessor.HttpContext!.Items;

		// Circular dependency: Handle it lke this: https://chatgpt.com/share/6a060551-37d0-83eb-97c8-c380ca2843eb (also view second reply regarding avoiding the chagneTracker in future)
		/*
		if (items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
			value is Queue<IDomainEvent> existingDomainEvents)
		{
			domainEventsQueue = existingDomainEvents;
		}
		else
		{
			domainEventsQueue = new Queue<IDomainEvent>();
		}*/
		if (items.TryGetValue("DomainEventsKey", out var value) &&
			value is Queue<IDomainEvent> existingDomainEvents)
			domainEventsQueue = existingDomainEvents;
		else
			domainEventsQueue = new Queue<IDomainEvent>();

		domainEvents.ForEach(domainEventsQueue.Enqueue);
		httpContextAccessor.HttpContext.Items["DomainEventsKey"] = domainEventsQueue;

		return result;
	}
}
