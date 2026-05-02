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
        
        return Result.Success<bool>(value: true);
    }
    
    internal Result<bool> AddTrainer(Guid trainerId)
    {
        if (_instructorIds.Contains(trainerId))
        {
            return Result.Failure<bool>(Error.Conflict(code: "", description: "Trainer already added to gym"));
        }

        _instructorIds.Add(trainerId);
        
        return Result.Success<bool>(value: true);
    }
    
    internal void RemoveCourt(Guid courtId)
    {
        _courtIds.Remove(courtId);
        DomainEvents.Add(new CourtRemovedFromClubEvent(this, courtId));
    }
    
    internal bool HasInstructor(Guid instructorId) => _instructorIds.Contains(instructorId);
    internal bool HasCourt(Guid courtId) => _courtIds.Contains(courtId);
}