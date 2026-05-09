using Microsoft.EntityFrameworkCore;
using UserAdministrationService.Application.Interfaces;
using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Persistence.Repositories;

internal sealed class UsersRepository(UserDbContext dbContext) : IUsersRepository
{
    public async Task AddUserAsync(User user)
    {
        await dbContext.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public async Task UpdateAsync(User user)
    {
        dbContext.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await dbContext.Users.AnyAsync(user => user.Email == email);
    }
}
