using UserAdministrationService.Domain.UserAggregate;

namespace UserAdministrationService.Application.Interfaces;

internal interface IUsersRepository
{
	Task AddUserAsync(User user, CancellationToken cancellationToken);
	Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
	Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
	Task UpdateAsync(User user, CancellationToken cancellationToken);
	Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
