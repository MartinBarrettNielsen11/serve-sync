using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Persistence;

internal sealed class UserDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (httpContextAccessor.HttpContext is null)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<RootAggregate>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();
        
        var result = await base.SaveChangesAsync(cancellationToken);
        
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

        return result;
    }
}