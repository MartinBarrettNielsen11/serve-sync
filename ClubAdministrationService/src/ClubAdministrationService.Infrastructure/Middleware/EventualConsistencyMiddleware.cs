using ClubAdministrationService.Domain.EventualConsistency;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace ClubAdministrationService.Infrastructure.Middleware;

internal class EventualConsistencyMiddleware(RequestDelegate next)
{
    public const string DomainEventsKey = "DomainEventsKey";
    
    public async Task InvokeAsync(HttpContext context, IPublisher publisher, ClubDbContext dbContext)
    {
        IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(context.RequestAborted);
        context.Response.OnCompleted(async () =>
        {
            try
            {
                /*if (context.Items.TryGetValue(DomainEventsKey, out var value))
                {
                    // as long as one can extract elements from queue/stack like data structure then publish said events
                }*/

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
