namespace domain.Player;

public class Player
{
    private readonly Guid _userId;
    //private readonly Schedule.Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();   
}