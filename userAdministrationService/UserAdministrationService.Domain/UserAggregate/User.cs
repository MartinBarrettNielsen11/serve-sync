using SharedKernel;

namespace UserAdministrationService.Domain.UserAggregate;

public class User : RootAggregate
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? PlayerId { get; private set; }
    public Guid? InstructorId { get; private set; }
    
    private readonly string _passwordHash = null!;
    
    public User(string firstName,
                string lastName,
                string email,
                string passwordHash,
                Guid? adminId = null,
                Guid? playerId = null,
                Guid? instructorId = null,
                Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AdminId = adminId;
        PlayerId = playerId;
        InstructorId = instructorId;
        _passwordHash = passwordHash;
    }
}