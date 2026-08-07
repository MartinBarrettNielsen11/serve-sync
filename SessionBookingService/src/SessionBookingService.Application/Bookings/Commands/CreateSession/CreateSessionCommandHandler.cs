using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.Common;
using SessionBookingService.Domain.CourtsAggregate;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateSession;

internal sealed class CreateSessionCommandHandler(
	ICourtsRepository courtsRepository,
	IInstructorsRepository instructorsRepository)
	: IRequestHandler<CreateSessionCommand, Result<Session>>
{
	public async ValueTask<Result<Session>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
	{
		Court? court = await courtsRepository.GetByIdAsync(command.CourtId, cancellationToken);

		if (court is null)
		{
			return Result.Failure<Session>(Error.NotFound("CourtNotFound", "Court not found"));
		}

		Instructor? instructor = await instructorsRepository.GetByIdAsync(command.InstructorId, cancellationToken);

		if (instructor is null)
		{
			return Result.Failure<Session>(Error.NotFound("InstructorNotFound", "Instructor not found"));
		}

		Result<TimeSlot> createTimeSlotResult = TimeSlot.FromDateTimes(command.StartDateTime,
			command.EndDateTime);

		// change returned error type to Error.Validation
		if (createTimeSlotResult is { IsFailure: true, Error.Type: ErrorType.Failure })
		{
			return Result.Failure<Session>(Error.NotFound("InvalidDateAndTime", "Invalid date and time"));
		}

		if (!instructor.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), createTimeSlotResult.Value))
		{
			return Result.Failure<Session>(Error.Conflict("InstructorsCalenderIsNotFreeForTheEntireDuration",
				"Instructor's calendar is not free for the entire session duration"));
		}

		// insert some time slot entry here,
		Session session = new(command.Name,
			command.Description,
			command.MaxPlayerCapacity,
			courtId: command.CourtId,
			instructorId: command.InstructorId,
			date: DateOnly.FromDateTime(command.StartDateTime),
			time: new TimeSlot(new TimeOnly(1), new TimeOnly(2)),
			categories: command.Categories);

		return Result.Success<Session>(session);
	}
}
