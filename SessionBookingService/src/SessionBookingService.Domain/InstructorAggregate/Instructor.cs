using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.InstructorAggregate;

internal sealed class Instructor : RootAggregate
{
    private Guid UserId { get; }
    private readonly List<Guid> _sessionIds = [];
    private readonly Schedule _schedule = Schedule.Empty();

    internal Instructor(Guid userId, 
                        Schedule? sch = null,
                        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        _schedule = sch ?? _schedule;
    }

    internal Result AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
        {
            return Result.Failure(Error.Conflict(
                code: "",
                description: "Session already exists in the schedule of the Instructor")
            );
        }
        
        Result bookingTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (bookingTimeSlotResult.IsFailure)
        {
            return Result.Failure(InstructorErrors.SessionCannotOverlap);
        }
        
        _sessionIds.Add(session.Id);
        return Result.Success();
    }
}
