using Microsoft.EntityFrameworkCore;
using SessionBookingService.Application.Common;
using SessionBookingService.Domain.InstructorAggregate;

namespace SessionBookingService.Infrastructure.Repositories;

internal sealed class InstructorsRepository(SessionBookingDbContext dbContext) : IInstructorsRepository
{
	public async Task AddInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
	{
		await dbContext.Instructors.AddAsync(instructor, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<Instructor?> GetByIdAsync(Guid instructorId, CancellationToken cancellationToken)
	{
		return await dbContext.Instructors.FirstOrDefaultAsync(instructor => instructor.Id == instructorId,
																cancellationToken);
	}

	public async Task UpdateAsync(Instructor instructor, CancellationToken cancellationToken)
	{
		dbContext.Instructors.Update(instructor);
		await dbContext.SaveChangesAsync(cancellationToken);
	}
}
