using Microsoft.EntityFrameworkCore;
using SessionBookingService.Domain.InstructorAggregate;
using SessionBookingService.Application.Common; 

namespace SessionBookingService.Persistence.Repositories;

internal sealed class InstructorsRepository(SessionBookingDbContext dbContext) : IInstructorsRepository
{
    public async Task AddInstructorAsync(Instructor instructor)
    {
        await dbContext.Instructors.AddAsync(instructor);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Instructor?> GetByIdAsync(Guid instructorId)
    {
        return await dbContext.Instructors.FirstOrDefaultAsync(instructor => instructor.Id == instructorId);
    }

    public async Task UpdateAsync(Instructor instructor)
    {
        dbContext.Instructors.Update(instructor);
        await dbContext.SaveChangesAsync();
    }
}