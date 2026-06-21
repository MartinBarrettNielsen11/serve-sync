using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Interfaces;

internal interface IUsersRepository
{
    Task AddUserAsync(User user, CancellationToken ct);
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
}