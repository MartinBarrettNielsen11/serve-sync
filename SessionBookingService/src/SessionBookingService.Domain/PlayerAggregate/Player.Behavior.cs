using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.PlayerAggregate;

internal sealed partial class Player : RootAggregate
{
	internal Result<bool> AddToSchedule(Session session)
	{
		if (_sessionIds.Contains(session.Id))
		{
			return Result.Failure<bool>(Error.Conflict("", "Session already exists in player's schedule"));
		}

		Result bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);

		if (bookTimeSlotResult.IsFailure)
		{
			if (bookTimeSlotResult.Error.Type == ErrorType.Conflict)
			{
				Result<bool> failure = Result.Failure<bool>(PlayerErrors.CannotHaveTwoOrMoreOverlappingSessions);
				return failure;
			}
			else
			{
				Result<bool> failure = Result.Failure<bool>(bookTimeSlotResult.Error);
				return failure;
			}
		}

		_sessionIds.Add(session.Id);
		return Result.Success(value: true);
	}

	public Result<bool> RemoveFromSchedule(Session session)
	{
		if (!_sessionIds.Contains(session.Id))
		{
			return Result.Failure<bool>(Error.NotFound("", "Session not found in player's schedule"));
		}

		Result<bool> removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
		if (removeBookingResult.IsFailure)
		{
			return Result.Failure<bool>(removeBookingResult.Error);
		}

		_sessionIds.Remove(session.Id);
		return Result.Success(value: true);
	}

	public bool HasBookingForSession(Guid sessionId) => _sessionIds.Contains(sessionId);
}
