using System;
using System.Collections.Generic;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;

namespace SessionBookingService.Domain.InstructorAggregate;

internal sealed partial class Instructor : RootAggregate
{
    private readonly List<Guid> _sessionIds = [];
    private readonly Schedule _schedule = Schedule.Empty();
    private Guid UserId { get; }
    
    internal Instructor(Guid userId, 
                        Schedule? sch = null,
                        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        UserId = userId;
        _schedule = sch ?? _schedule;
    }

    private Instructor() { } // For EF / serialization
}
