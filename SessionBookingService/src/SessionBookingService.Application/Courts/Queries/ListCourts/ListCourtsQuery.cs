using Mediator;
using SessionBookingService.Domain.CourtsAggregate;
using SharedKernel.Results;

namespace SessionBookingService.Application.Courts.Queries.ListCourts;

internal sealed record ListCourtsQuery(Guid ClubId) : IRequest<Result<List<Court>>>;
