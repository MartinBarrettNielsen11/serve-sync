using System.Runtime.CompilerServices;
using ClubAdministrationService.Application.Common.Interfaces;
using ClubAdministrationService.Application.Courts.Commands.DeleteCourt;
using ClubAdministrationService.Domain.ClubAggregate;
using ClubAdministrationService.Domain.SubscriptionAggregate;
using SharedKernel.Results;

namespace ClubAdministrationService.Application.Clubs.AddInstructor;

// ReSharper disable once UnusedType.Global
internal sealed class AddInstructorCommandHandler(IClubsRepository clubsRepository, ISubscriptionsRepository subscriptionsRepository)
{
    internal async ValueTask<Result> Handle(AddInstructorCommand command, CancellationToken cancellationToken)
    {
        Subscription? subscription = await subscriptionsRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Result.Failure(Error.NotFound(code: "SubscriptionNotFound", description: "Subscription not found"));
        }
        
        if (!subscription.HasGym(command.ClubId))
        {
            return Result.Failure(Error.NotFound(code: "ClubNotFound", description: "Club not found"));
        }

        Club? club = await clubsRepository.GetByIdAsync(command.ClubId);
        
        if (club is null)
        {
            return Result.Failure(Error.NotFound(code: "ClubNotFound", description: "Club not found"));
        }

        Result<bool> addInstructorResult = club.AddInstructor(command.InstructorId);

        if (addInstructorResult.IsFailure)
        {
            return Result.Failure(addInstructorResult.Error);
        }
        
        await clubsRepository.UpdateAsync(club);

        return Result.Success<bool>(value: true);
    }
}