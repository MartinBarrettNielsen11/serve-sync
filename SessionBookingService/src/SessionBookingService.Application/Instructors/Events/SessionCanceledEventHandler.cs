using MediatR;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.CourtsAggregate.Events;
using SessionBookingService.Domain.EventualConsistency;
using SessionBookingService.Domain.InstructorAggregate;

namespace SessionBookingService.Application.Instructors.Events;

internal sealed class SessionCanceledEventHandler(IInstructorsRepository instructorsRepository) : INotificationHandler<SessionCanceledForCourtEvent>
{
    public async Task Handle(SessionCanceledForCourtEvent notification, CancellationToken cancellationToken)
    {
        Instructor trainer = await instructorsRepository.GetByIdAsync(notification.Session.InstructorId)
                             ?? throw new EventualConsistencyException(SessionCanceledForCourtEvent.InstructorNotFound);

        var removeFromScheduleResult = trainer.RemoveFromSchedule(notification.Session);

        if (removeFromScheduleResult.IsFailure)
        {
            // throw something in here
        }

        await instructorsRepository.UpdateAsync(trainer);
    }
}
