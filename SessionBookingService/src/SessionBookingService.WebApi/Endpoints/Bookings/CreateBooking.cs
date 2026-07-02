using Asp.Versioning;
using Asp.Versioning.Builder;
using Mediator;
using SessionBookingService.Application.Bookings.Commands.CreateBooking;
using SessionBookingService.Contracts.Bookings;
using SessionBookingService.WebApi.Infrastructure;
using SharedKernel.Results;

namespace SessionBookingService.WebApi.Endpoints.Bookings;

public sealed class CreateBooking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(pattern: "/sessions/{sessionId:guid}/bookings",
                handler: async (CreateBookingRequest request,
                    Guid sessionId,
                    ISender sender,
                    CancellationToken cancellationToken) =>
                {
                    CreateBookingCommand command = new(sessionId, request.PlayerId);

                    Result createBookingResult = await sender.Send(command, cancellationToken);

                    IResult response = createBookingResult.Match(
                        onSuccess: Results.NoContent,
                        onFailure: err => ProblemDetailsMapper.Problem(errors: [err.Error]));

                    return response;
                })
            .WithTags(Tags.Bookings)
            .WithSummary("Create booking")
            .WithDescription("Create booking for a session");
        //.RequireAuthorization();
    }
}