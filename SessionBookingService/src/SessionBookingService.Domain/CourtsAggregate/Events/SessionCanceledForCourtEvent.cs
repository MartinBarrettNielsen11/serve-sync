using SessionBookingService.Domain.EventualConsistency;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel;
using SharedKernel.Results;

namespace SessionBookingService.Domain.CourtsAggregate.Events;

#pragma warning disable MSG0005
internal sealed record SessionCanceledForCourtEvent(Session Session) : IDomainEvent
#pragma warning restore MSG0005
{
	public static readonly Result InstructorNotFound =
		EventualConsistencyError.From("SessionCanceledEvent.InstructorNotFound",
									"Instructor not found");
}
