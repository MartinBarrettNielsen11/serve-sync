using MediatR;
using SharedKernel.Results;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Profiles.CreatePlayerProfile;

internal sealed class CreatePlayerProfileCommandHandler(IUsersRepository usersRepository) : IRequestHandler<CreatePlayerProfileCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePlayerProfileCommand command, CancellationToken cancellationToken)
    {
        User? user = await usersRepository.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(Error.NotFound(code: "UserNotFound", description: "User not found"));
        }
        
        Result<Guid> createParticipantProfileResult = user.CreatePlayerProfile();

        await usersRepository.UpdateAsync(user, cancellationToken);

        return createParticipantProfileResult;
    }
}