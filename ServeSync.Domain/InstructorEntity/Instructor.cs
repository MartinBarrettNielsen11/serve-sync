using ServeSync.Domain.ScheduleEntity;

namespace ServeSync.Domain.InstructorEntity;

public class Instructor
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule _schedule = Schedule.Empty();
}