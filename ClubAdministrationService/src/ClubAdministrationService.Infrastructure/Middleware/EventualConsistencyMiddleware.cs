using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClubAdministrationService.Infrastructure.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    public const string DomainEventsKey = "DomainEventsKey";

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, ClubDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value)) // && value is Queue<IDomainEvent> domainEvents)
                {
                    // as long as one can extract elements from queue/stack like data structure then publish said events
                }

                await transaction.CommitAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await next(context);
    }
}
