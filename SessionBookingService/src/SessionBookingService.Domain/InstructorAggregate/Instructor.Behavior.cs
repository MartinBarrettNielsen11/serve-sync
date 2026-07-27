using SessionBookingService.Domain.Common;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Domain.InstructorAggregate;

internal sealed partial class Instructor
{
	internal Result<bool> AddSessionToSchedule(Session session)
	{
		if (_sessionIds.Contains(session.Id))
		{
			return Result.Failure<bool>(Error.Conflict(
				code: "",
				description: "Session already exists in the schedule of the Instructor")
			);
		}

		Result bookingTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

		if (bookingTimeSlotResult.IsFailure)
        {
            return Result.Failure<bool>(InstructorErrors.SessionCannotOverlap);
        }

        _sessionIds.Add(session.Id);
		return Result.Success(true);
	}

	public bool IsTimeSlotFree(DateOnly date, TimeSlot time) => _schedule.CanBookTimeSlot(date, time);

	public Result<bool> RemoveFromSchedule(Session session)
	{
		if (!_sessionIds.Contains(session.Id))
        {
            return Result.Failure<bool>(Error.NotFound("", "Session not found in instructors's schedule"));
        }

        Result<bool> removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);

		if (removeBookingResult.IsFailure)
        {
            return Result.Failure<bool>(removeBookingResult.Error);
        }

        _sessionIds.Remove(session.Id);
		return Result.Success(true);
	}
}
