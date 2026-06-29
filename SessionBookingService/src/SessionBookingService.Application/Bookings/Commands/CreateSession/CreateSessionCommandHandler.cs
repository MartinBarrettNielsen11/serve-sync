using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateSession;

internal sealed class CreateSessionCommandHandler(ICourtsRepository courtsRepository, IInstructorsRepository instructorsRepository)
    : IRequestHandler<CreateSessionCommand, Result<Session>>
{
    public async ValueTask<Result<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        Court? court = await courtsRepository.GetByIdAsync(command.CourtId, cancellationToken);

        if (court is null)
        {
            return Result.Failure<Session>(Error.NotFound(code: "CourtNotFound", description: "Court not found"));
        }
        
        Instructor? instructor = await instructorsRepository.GetByIdAsync(command.InstructorId, cancellationToken);
        
        if (instructor is null)
        {
            return Result.Failure<Session>(Error.NotFound(code: "InstructorNotFound", description: "Instructor not found"));
        }
        
        // insert some time slot entry here,
        Session session = new(name: command.Name,
                              description: command.Description,
                              maxPlayerCapacity: command.MaxPlayerCapacity,
                              courtId: command.CourtId,
                              instructorId: command.InstructorId,
                              date: DateOnly.FromDateTime(command.StartDateTime),
                              time: new TimeSlot(new TimeOnly(1), new TimeOnly(2)),
                              categories: command.Categories);

        return session;
    }
}
