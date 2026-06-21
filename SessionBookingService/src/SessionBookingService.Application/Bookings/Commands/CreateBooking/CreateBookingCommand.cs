using MediatR;
using SharedKernel.Results;

namespace SessionBookingService.Application.Bookings.Commands.CreateBooking;

internal sealed record CreateBookingCommand(Guid SessionId, Guid PlayerId) : IRequest<Result<Guid>>;