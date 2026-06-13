using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using SessionBookingService.Domain.EventualConsistency;
using SessionBookingService.Persistence;
using SharedKernel;

namespace SessionBookingService.Infrastructure.Middlewares;

internal sealed class EventualConsistencyMiddleware(RequestDelegate next)
{
    internal const string DomainEventsKey = "DomainEventsKey";

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, SessionBookingDbContext dbContext)
    {
        IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(context.RequestAborted);
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                {
                    while (domainEvents.TryDequeue(out IDomainEvent? nextEvent))
                    {
                        await publisher.Publish(nextEvent, context.RequestAborted);
                    }
                }

                await transaction.CommitAsync(context.RequestAborted);
            }
            catch (EventualConsistencyException)
            {
                // handle eventual consistency exception
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await next(context);
    }
}
