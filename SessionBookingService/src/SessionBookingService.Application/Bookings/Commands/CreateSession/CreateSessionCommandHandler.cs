using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateSession;

internal sealed class CreateSessionCommandHandler(ICourtsRepository courtsRepository, IInstructorsRepository instructorsRepository)
{
    internal async ValueTask<Result<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        Court? court = await courtsRepository.GetByIdAsync(command.CourtId);

        if (court is null)
        {
            return Result.Failure<Session>(Error.NotFound(code: "CourtNotFound", description: "Court not found"));
        }
        
        Instructor? instructor = await instructorsRepository.GetByIdAsync(command.InstructorId);
        
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
