using System;

namespace SessionBookingService.Application.Bookings.Commands.CreateBooking;

internal sealed record CreateBookingCommand(Guid SessionId, Guid PlayerId);