namespace domain.Instructor;

public class Instructor
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    private readonly Schedule.Schedule _schedule = Schedule.Empty();
}