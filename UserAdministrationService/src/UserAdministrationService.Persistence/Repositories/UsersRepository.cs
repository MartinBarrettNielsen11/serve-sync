using Microsoft.EntityFrameworkCore;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Persistence.Repositories;

internal sealed class UsersRepository(UserDbContext dbContext) : IUsersRepository
{
    public async Task AddUserAsync(User user, CancellationToken ct)
    {
        await dbContext.AddAsync(user, ct);
        await dbContext.SaveChangesAsync(ct);
    }
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, ct);
    }

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId, ct);
    }

    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        dbContext.Update(user);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
    {
        return await dbContext.Users.AnyAsync(user => user.Email == email, ct);
    }
}
