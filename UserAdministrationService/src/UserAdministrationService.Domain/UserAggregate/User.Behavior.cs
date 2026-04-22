using SharedKernel;
using SharedKernel.Results;
using UserAdministrationService.Domain.Interfaces;

namespace UserAdministrationService.Domain.UserAggregate;

internal sealed partial class User : RootAggregate
{
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
        
        return Result.Success<Guid>(AdminId.Value);
    }

    public Result<Guid> CreatePlayerProfile()
    {
        if (PlayerId is not null)
        {
            return Result.Failure<Guid>(Error.Conflict(code: "", description: "User already has a participant profile"));
        }

        PlayerId = Guid.CreateVersion7();
        // Add PlayerProfileCreatedEvent to domain events
        
        return Result.Success<Guid>(PlayerId.Value);
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
}