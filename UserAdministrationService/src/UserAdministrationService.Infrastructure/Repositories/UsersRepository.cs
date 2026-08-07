using Microsoft.EntityFrameworkCore;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Infrastructure.Repositories;

internal sealed class UsersRepository(UserDbContext dbContext) : IUsersRepository
{
	public async Task AddUserAsync(User user, CancellationToken cancellationToken)
	{
		await dbContext.AddAsync(user, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
	{
		return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
	}

	public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
	}

	public async Task UpdateAsync(User user, CancellationToken cancellationToken)
	{
		dbContext.Update(user);
		await dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
	{
		return await dbContext.Users.AnyAsync(user => user.Email == email, cancellationToken);
	}
}
