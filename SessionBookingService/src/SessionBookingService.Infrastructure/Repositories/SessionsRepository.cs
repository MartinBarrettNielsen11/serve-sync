using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Infrastructure.Repositories;

internal sealed class SessionsRepository(SessionBookingDbContext dbContext) : ISessionsRepository
{
	public async Task AddSessionAsync(Session session, CancellationToken cancellationToken)
	{
		await dbContext.Sessions.AddAsync(session, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		return await dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
	}

	public async Task UpdateAsync(Session session, CancellationToken cancellationToken)
	{
		dbContext.Update(session);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task Remove(Session session, CancellationToken cancellationToken)
	{
		dbContext.Sessions.Remove(session);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<List<Session>> ListByIds(
		IReadOnlyList<Guid> sessionIds,
		DateTime? startDateTime = null,
		DateTime? endDateTime = null,
		List<SessionCategory>? categories = null)
	{
		return await dbContext.Sessions
			.AsNoTracking()
			.Where(session => sessionIds.Contains(session.Id))
			.WhereBetweenDateAndTimes(startDateTime, endDateTime)
			.ToListAsync();
	}
}


file static class DbContextSessionExtensions
{
	public static IQueryable<Session> WhereBetweenDateAndTimes(this IQueryable<Session> query, DateTime? start,
		DateTime? end)
	{
		if (start is null && end is null)
		{
			return query;
		}

		start ??= DateTime.MinValue;
		end ??= DateTime.MaxValue;

		IQueryable<Session> result = query
			.AsNoTracking()
			.Where(session => session.Date >= DateOnly.FromDateTime(start.Value) &&
							  session.Date <= DateOnly.FromDateTime(end.Value))
			.Where(session => session.Time.Start >= TimeOnly.FromDateTime(start.Value));

		return result;
	}
}
