using System;
using System.Collections.Generic;
using SharedKernel;

namespace SessionBookingService.Domain.SessionAggregate;

internal sealed partial class Session
{
    public Guid InstructorId { get; }
    private readonly List<Booking> _bookings = new();
    
    public int MaxPlayerCapacity { get;}
    public DateOnly Date { get; }
    public TimeSlot Time { get; }
    public string Name { get; } = null!;
    public string Description { get; } = null!;

    public Session(string name,
        string description,
        int maxPlayerCapacity,
        Guid instructorId,
        DateOnly date,
        TimeSlot time,
        Guid? id = null) : base(id ?? Guid.CreateVersion7())
    {
        Name = name;
        Description = description;
        InstructorId = instructorId;
        Date = date;
        Time = time;
        MaxPlayerCapacity = maxPlayerCapacity;
    }

    private Session() { } // For EF / serialization
}