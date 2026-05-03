using System;
using System.Threading.Tasks;
using SessionBookingService.Domain.SessionAggregate;

namespace SessionBookingService.Application.Common;

internal interface ISessionsRepository
{
    Task AddSessionAsync(Session session);
    Task<Session?> GetByIdAsync(Guid id);
    Task UpdateAsync(Session session);
    Task Remove(Session session);
}