using MediatR;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreatePlayerProfile;

internal sealed class CreatePlayerProfileCommandHandler(IUsersRepository usersRepository) : IRequestHandler<CreatePlayerProfileCommand, Result<Guid>>
{
#pragma warning disable CA1822
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<Guid>> Handle(CreatePlayerProfileCommand command, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CA1822
    {
        User? user = await usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Result.Failure<Guid>(Error.NotFound(code: "UserNotFound", description: "User not found"));
        }
        
        Result<Guid> createParticipantProfileResult = user.CreatePlayerProfile();

        await usersRepository.UpdateAsync(user);

        return createParticipantProfileResult;
    }
}