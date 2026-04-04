using SharedKernel;
using SharedKernel.Results;

namespace SessionReservationService.Domain.CourtAggregate;

public sealed class Court : RootAggregate
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

    internal Result ScheduleSession(Guid sessionId)
    {
        if (_sessionIds.Exists(x => x == sessionId))
        {
            return Result.Failure(Error.Failure(code: "yo", description: "Session already exists in court"));
        }
        
        if (_maxDailySessions < _sessionIds.Count)
        {
            return Result.Failure(CourtErrors.NumberOfSessionsCannotExceedSubscriptionLimit);
        }
        
        _sessionIds.Add(sessionId);
        
        return Result.Success();
    }
    
    public bool HasSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }
}
