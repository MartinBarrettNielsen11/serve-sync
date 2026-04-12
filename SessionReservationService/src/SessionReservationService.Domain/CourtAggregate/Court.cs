using SessionReservationService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionReservationService.Domain.CourtAggregate;

internal sealed class Court : RootAggregate
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Guid _clubId;
    private readonly Schedule _schedule = Schedule.Empty();
    public string Name { get; } = null!;

    public Court(
        string name,
        int maxDailySessions,
        Guid clubId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        _clubId = clubId;
        _schedule = schedule ?? Schedule.Empty();
    }

    internal Result ScheduleSession(Session session)
    {
        if (_sessionIds.Exists(x => x == session.Id))
        {
            return Result.Failure(Error.Failure(code: "yo", description: "Session already exists in court"));
        }
        
        if (_sessionIds.Count >= _maxDailySessions)
        {
            return Result.Failure(CourtErrors.NumberOfSessionsCannotExceedSubscriptionLimit);
        }
        
        Result bookingResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookingResult.IsFailure)
        {
            return Result.Failure(CourtErrors.SessionsCannotOverlap);
        }
        // return error result if overlapping
        
        _sessionIds.Add(session.Id);
        
        return Result.Success();
    }
    
    public bool HasSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }
}
