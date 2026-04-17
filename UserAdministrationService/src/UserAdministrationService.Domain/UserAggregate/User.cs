using SharedKernel;
using SharedKernel.Results;
using UserAdministrationService.Domain.Interfaces;

namespace UserAdministrationService.Domain.UserAggregate;

internal sealed class User : RootAggregate
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
    
    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public Result<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
        {
            return Result.Failure<Guid>(Error.Conflict(code: "", description: "User already has an admin profile"));
        }

        AdminId = Guid.CreateVersion7();
        
        // Add AdminProfileCreatedEvent to domain events
        
        return Result.Success(AdminId.Value);
    }

    public Result<Guid> CreatePlayerProfile()
    {
        if (PlayerId is not null)
        {
            return Result.Failure<Guid>(Error.Conflict(code: "", description: "User already has a participant profile"));
        }

        PlayerId = Guid.CreateVersion7();
        
        // Add PlayerProfileCreatedEvent to domain events
        
        return Result.Success(PlayerId.Value);
    }

    public Result<Guid> CreateTrainerProfile()
    {
        if (InstructorId is not null)
        {
            return Result.Failure<Guid>(Error.Conflict(code: "", description: "User already has an instructor profile"));
        }
        
        InstructorId = Guid.CreateVersion7();

        // Add InstructorCreatedEvent to domain events
        
        return Result.Success(InstructorId.Value);
    }
    
    private User()
    {
    }

}