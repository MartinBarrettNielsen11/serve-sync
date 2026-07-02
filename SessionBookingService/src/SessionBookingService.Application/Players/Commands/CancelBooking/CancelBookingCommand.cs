using Mediator;
using SharedKernel.Results;

namespace SessionBookingService.Application.Players.Commands.CancelBooking;

public sealed record CancelBookingCommand(Guid PlayerId, Guid SessionId) : IRequest<Result>;