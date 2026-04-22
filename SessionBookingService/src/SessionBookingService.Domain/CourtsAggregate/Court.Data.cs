using System;
using System.Collections.Generic;
using SharedKernel;

namespace SessionBookingService.Domain.CourtsAggregate;

internal sealed partial class Court
{
    private readonly List<Guid> _sessionIds = new();
    private readonly int _maxDailySessions;
    private readonly Schedule _schedule = Schedule.Empty();
    public string Name { get; } = null!;
    public Guid ClubId { get; }
    public IReadOnlyList<Guid> SessionIds => _sessionIds.AsReadOnly();

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

    private Court() { } // For EF / serialization
}