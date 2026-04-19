using System;
using System.Collections.Generic;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.PlayerAggregate;

internal sealed class Player : RootAggregate
{
    public Guid UserId { get; }
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Player(Guid userId,
                  Schedule? schedule = null,
                  Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }
    
    // intermediate placeholder for testing "Result"
    internal Result<bool> AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Result.Failure<bool>(Error.Conflict(code: "", description: "Session already exists in player's schedule"));
        }

        Result bookTimeSlotResult = _schedule.BookTimeSlot(
            session.Date,
            session.Time);

        if (bookTimeSlotResult.IsFailure)
        {
            return bookTimeSlotResult.Error.Type == ErrorType.Conflict
                ? Result.Failure<bool>(PlayerErrors.CannotHaveTwoOrMoreOverlappingSessions)
                : Result.Failure<bool>(bookTimeSlotResult.Error);
        }

        _sessionIds.Add(session.Id);
        return Result.Success<bool>(value: true);
    }

    public Result<bool> RemoveFromSchedule(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
        {
            return Result.Failure<bool>(Error.NotFound(code: "", description: "Session not found in player's schedule"));
        }

        var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
        if (removeBookingResult.IsFailure)
        {
            // return some error
        }

        _sessionIds.Remove(session.Id);
        return Result.Success<bool>(true);
    }
}
