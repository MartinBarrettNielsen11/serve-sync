using System;
using System.Collections.Generic;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate;

internal sealed class Court : RootAggregate
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Schedule _schedule = Schedule.Empty();
    public string Name { get; } = null!;
    public Guid ClubId { get; }


    public Court(
        string name,
        int maxDailySessions,
        Guid clubId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        ClubId = clubId;
        _schedule = schedule ?? Schedule.Empty();
    }

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
        
        Result bookingResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookingResult.IsFailure)
        {
            return Result.Failure<bool>(CourtErrors.SessionsCannotOverlap);
        }
        // return error result if overlapping
        
        _sessionIds.Add(session.Id);
        
        return Result.Success<bool>(value: true);
    }
    
    public bool HasSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }
}
