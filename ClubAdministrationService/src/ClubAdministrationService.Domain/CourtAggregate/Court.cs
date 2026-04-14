using SharedKernel;

namespace ClubAdministrationService.Domain.CourtAggregate;

internal sealed class Court : RootAggregate
{
    public string Name { get; } = null!;

    public Guid ClubId { get; }
    public int MaxDailySessions { get; }
    
    public Court(
        string name,
        Guid clubId,
        int maxDailySessions,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        ClubId = clubId;
        MaxDailySessions = maxDailySessions;
    }
}