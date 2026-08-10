using Mediator;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate.Events;
using SessionBookingService.Domain.EventualConsistency;
using SessionBookingService.Domain.InstructorAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Sessions.IntegrationEvents;

internal sealed class SessionCanceledEventHandler(IInstructorsRepository instructorsRepository)
	: INotificationHandler<SessionCanceledForCourtEvent>
{
	public async ValueTask Handle(SessionCanceledForCourtEvent notification, CancellationToken cancellationToken)
	{
		Instructor instructor =
			await instructorsRepository.GetByIdAsync(notification.Session.InstructorId, cancellationToken)
			?? throw new EventualConsistencyException(SessionCanceledForCourtEvent.InstructorNotFound);

		Result<bool> removeFromScheduleResult = instructor.RemoveFromSchedule(notification.Session);

		if (removeFromScheduleResult.IsFailure)
		{
			// throw something in here
		}

		await instructorsRepository.UpdateAsync(instructor, cancellationToken);
	}
}
