using Mediator;
using SessionBookingService.Domain.SessionAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateSession;

internal sealed record CreateSessionCommand(Guid CourtId,
											string Name,
											string Description,
											int MaxPlayerCapacity,
											DateTime StartDateTime,
											DateTime EndDateTime,
											Guid InstructorId,
											List<SessionCategory> Categories) : IRequest<Result<Session>>;
