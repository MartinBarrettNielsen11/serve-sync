namespace UserAdministrationService.Domain.UserAggregate;

public class User
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? PlayerId { get; private set; }
    public Guid? InstructorId { get; private set; }
}