using SharedKernel;
using SharedKernel.Results;
using UserAdministrationService.Domain.Interfaces;
using UserAdministrationService.Domain.UserAggregate.Events;

namespace UserAdministrationService.Domain.UserAggregate;

internal sealed partial class User : RootAggregate
{
	internal Result<Guid> CreateAdminProfile()
	{
		if (AdminId is not null) return Result.Failure<Guid>(Error.Conflict("", "User already has an admin profile"));

		AdminId = Guid.CreateVersion7();
		DomainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

		return Result.Success(AdminId.Value);
	}

	internal Result<Guid> CreatePlayerProfile()
	{
		if (PlayerId is not null)
			return Result.Failure<Guid>(Error.Conflict("", "User already has a participant profile"));

		PlayerId = Guid.CreateVersion7();
		DomainEvents.Add(new PlayerProfileCreatedEvent(Id, PlayerId.Value));

		return Result.Success(PlayerId.Value);
	}

	internal Result<Guid> CreateInstructorProfile()
	{
		if (InstructorId is not null)
			return Result.Failure<Guid>(Error.Conflict("", "User already has an instructor profile"));

		InstructorId = Guid.CreateVersion7();
		DomainEvents.Add(new InstructorProfileCreatedEvent(Id, InstructorId.Value));

		return Result.Success(InstructorId.Value);
	}

	internal bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
	{
		return passwordHasher.IsCorrectPassword(password, _passwordHash);
	}
}