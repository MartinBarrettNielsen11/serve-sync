using ClubAdministrationService.Domain.ClubAggregate.Events;
using ClubAdministrationService.Domain.CourtAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace ClubAdministrationService.Domain.ClubAggregate;

internal sealed partial class Club : RootAggregate
{
	internal Result<bool> AddCourt(Court court)
	{
		if (_courtIds.Contains(court.Id))
        {
            return Result.Failure<bool>(ClubErrors.CourtAlreadyExistsInClub);
        }

        if (_maxCourtCapacity <= _courtIds.Count)
        {
            return Result.Failure<bool>(ClubErrors.NumberOfCourtsCannotExceedSubscriptionLimit);
        }

        _courtIds.Add(court.Id);

		DomainEvents.Add(new CourtAddedToClubEvent(this, court));

		return Result.Success(true);
	}

	internal Result<bool> AddInstructor(Guid instructorId)
	{
		if (_instructorIds.Contains(instructorId))
        {
            return Result.Failure<bool>(Error.Conflict("", "Instructor already added to club"));
        }

        _instructorIds.Add(instructorId);

		return Result.Success(true);
	}

	internal void RemoveCourt(Guid courtId)
	{
		_courtIds.Remove(courtId);
		DomainEvents.Add(new CourtRemovedFromClubEvent(this, courtId));
	}

	internal bool HasInstructor(Guid instructorId)
	{
		return _instructorIds.Contains(instructorId);
	}

	internal bool HasCourt(Guid courtId)
	{
		return _courtIds.Contains(courtId);
	}
}
