using SessionBookingService.Domain.InstructorAggregate;

namespace SessionBookingService.Application.Common;

internal interface IInstructorsRepository
{
    Task AddInstructorAsync(Instructor instructor, CancellationToken cancellationToken);
    Task<Instructor?> GetByIdAsync(Guid instructorId, CancellationToken cancellationToken);
    Task UpdateAsync(Instructor instructor, CancellationToken cancellationToken);
}