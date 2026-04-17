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
    internal Result AddToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Result.Failure(Error.Conflict(code: "", description: "Session already exists in player's schedule"));
        }

        Result bookTimeSlotResult = _schedule.BookTimeSlot(
            session.Date,
            session.Time);

        if (bookTimeSlotResult.IsFailure)
        {
            return bookTimeSlotResult.Error.Type == ErrorType.Conflict
                ? Result.Failure(PlayerErrors.CannotHaveTwoOrMoreOverlappingSessions)
                : Result.Failure(bookTimeSlotResult.Error);
        }

        _sessionIds.Add(session.Id);
        return Result.Success();
    }

    public Result RemoveFromSchedule(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
        {
            return Result.Failure(Error.NotFound(code: "", description: "Session not found in player's schedule"));
        }

        var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
        if (removeBookingResult.IsFailure)
        {
            // return some error
        }

        _sessionIds.Remove(session.Id);
        return Result.Success();
    }
}
