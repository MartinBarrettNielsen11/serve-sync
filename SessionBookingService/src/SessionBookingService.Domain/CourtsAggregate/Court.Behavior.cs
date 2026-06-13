using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate;

internal sealed partial class Court : RootAggregate
{
    internal Result<bool> ScheduleSession(Session session)
    {
        if (_sessionIds.Exists(x => x == session.Id))
        {
            return Result.Failure<bool>(Error.Failure(code: "yo", description: "Session already exists in court"));
        }
        
        if (_sessionIds.Count >= _maxDailySessions)
        {
            return Result.Failure<bool>(CourtErrors.NumberOfSessionsCannotExceedSubscriptionLimit);
        }
        
        Result bookingResult = _schedule.BookTimeSlot(session.Date, 
                                                      session.Time);
        if (bookingResult.IsFailure)
        {
            return Result.Failure<bool>(CourtErrors.SessionsCannotOverlap);
        }
        
        _sessionIds.Add(session.Id);
        // some event will be needed here
        
        return Result.Success<bool>(value: true);
    }
    
    public bool HasSession(Guid sessionId) => _sessionIds.Contains(sessionId);
}
