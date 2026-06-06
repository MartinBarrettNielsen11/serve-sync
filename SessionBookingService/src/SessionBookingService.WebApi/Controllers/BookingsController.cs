using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionBookingService.Application.Bookings.Commands.CreateBooking;
using SessionBookingService.Contracts.Bookings;
using SharedKernel.Results;

#pragma warning disable S3261
namespace SessionBookingService.WebApi.Controllers;
#pragma warning restore S3261

/// <summary>
/// This has to be replaced
/// </summary>
/// <param name="sender"></param>
[Route("sessions/{sessionId:guid}/bookings")]
public class BookingsController(ISender sender) : ApiController
{
    /// <summary> Creates a new club </summary>
    /// <remarks>
    /// Creates a club for the specified subscription.
    /// The caller must have permission to create clubs within the subscription.
    /// </remarks>
    /// <param name="request"> The club creation request containing the club name, description, and settings </param>
    /// <param name="sessionId"> The subscription identifier that owns the club </param>
    /// <param name="cancellationToken"></param>
    /// <returns> The newly created club </returns>
    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingRequest request, Guid sessionId, CancellationToken cancellationToken)
    {
        CreateBookingCommand command = new(sessionId, request.ParticipantId);

        Result createBookingResult = await sender.Send(command, cancellationToken);
        
        var result = createBookingResult.Match(
            onSuccess: _ => NoContent(),
            onFailure: errors => Problem([errors]));

        return result;
    }
}