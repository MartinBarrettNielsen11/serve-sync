using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Interfaces;

internal interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);
    Task UpdateAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
}