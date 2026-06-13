using SessionBookingService.Domain.InstructorAggregate;

namespace SessionBookingService.Application.Common;

internal interface IInstructorsRepository
{
    Task AddInstructorAsync(Instructor instructor);
    Task<Instructor?> GetByIdAsync(Guid instructorId);
    Task UpdateAsync(Instructor instructor);
}