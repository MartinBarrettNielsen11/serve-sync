using SessionBookingService.Domain.EventualConsistency;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate.Events;

internal sealed record SessionCanceledForCourtEvent(Session Session) : IDomainEvent
{
    public static readonly Result InstructorNotFound = EventualConsistencyError.From(
        code: "SessionCanceledEvent.InstructorNotFound",
        description: "Instructor not found");
}